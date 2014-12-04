using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.DevIl;
using Tao.OpenGl;

namespace Shield3D
{
    // класс LIMB отвечает за логические единицы 3D объектов в загружаемой сцене
    public class LIMB
    {
        // при инициализации мы должны указать количество вершин (vertex) и полигонов (face) которые описывают геометри под-объекта
        public LIMB(int a, int b)
        {
            // записываем количество вершин и полигонов
            VandF[0] = a;
            VandF[1] = b;
            ModelHasTexture = false;

            // выделяем память
            memcompl();
        }

        public int Result; // флаг успешности

        // массивы для хранения данных (геометрии и текстурных координат)
        public float[,] vert;
        public int[,] face;
        public float[,] t_vert;
        public int[,] t_face;

        // номер материала (текстуры) данного под-объекта
        private int MaterialNum = -1;

        // временное хранение информации
        public int[] VandF = new int[4];

        public bool ModelHasTexture { private set; get; }
            
        public void SetMaterialNum(int new_num)
        {
            MaterialNum = new_num;
            if (MaterialNum > -1)
            {
                ModelHasTexture = true;
            }
        }

        // массивы для текстурных координат
        public void createTextureVertexMem(int a)
        {
            VandF[2] = a;
            t_vert = new float[3, VandF[2]];
        }

        // привязка значений текстурных координат к полигонам 
        public void createTextureFaceMem(int b)
        {
            VandF[3] = b;
            t_face = new int[3, VandF[3]];
        }

        // память для геометрии
        private void memcompl()
        {
            vert = new float[3, VandF[0]];
            face = new int[3, VandF[1]];
        }

        // номер текстуры
        public int GetTextureNum()
        {
            return MaterialNum;
        }
    };

    // класс для работы с текстурами
    public class TexturesForObjects
    {
        public TexturesForObjects()
        {

        }

        // имя текстуры
        private string texture_name = "";
        // ее ID
        private int imageId = 0;

        // идетификатор текстуры в памяти openGL
        private uint mGlTextureObject = 0;

        // получение этого идентификатора
        public uint GetTextureObj()
        {
            return mGlTextureObject;
        }

        // загрузка текстуры
        public void LoadTextureForModel(string FileName)
        {
            // запоминаем имя файла
            texture_name = FileName;
            // создаем изображение с индификатором imageId 
            Il.ilGenImages(1, out imageId);
            // делаем изображение текущим 
            Il.ilBindImage(imageId);

            string url = texture_name;

            // если загрузка удалась
            if (Il.ilLoadImage(url))
            {
                // если загрузка прошла успешно 
                // сохраняем размеры изображения 
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);

                // определяем число бит на пиксель 
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);

                switch (bitspp)// в зависимости оп полученного результата 
                {
                    // создаем текстуру используя режим GL_RGB или GL_RGBA 
                    case 24:
                        mGlTextureObject = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        mGlTextureObject = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
                // очищаем память 
                Il.ilDeleteImages(1, ref imageId);
            }
        }

        // создание текстуры в панями openGL 
        private static uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            // индетефекатор текстурного объекта 
            uint texObject;

            // генерируем текстурный объект 
            Gl.glGenTextures(1, out texObject);

            // устанавливаем режим упаковки пикселей 
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

            // создаем привязку к только что созданной текстуре 
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);

            // устанавливаем режим фильтрации и повторения текстуры 
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

            // создаем RGB или RGBA текстуру
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Format, w, h, 0, Format, Gl.GL_UNSIGNED_BYTE, pixels);
            
            // возвращаем индетефекатор текстурного объекта 
            return texObject;
        }
    }
    
    public class Model
    {
        public LIMB Object {set; get;}
        public List<TexturesForObjects> Textures { set; get; }
        public bool IsLoad { set; get; }

        public int ListID { set; get; }

        public Vector3D coord { set; get; }

        public Model()
        {
            coord = new Vector3D();
        }
        //#region Helpers
        //public void SetMinimum(float x, float y, float z)
        //{
        //    coord.minimum[0] = x;
        //    coord.minimum[1] = y;
        //    coord.minimum[2] = z;
        //}

        //public void SetMaximum(float x, float y, float z)
        //{
        //    coord.maximum[0] = x;
        //    coord.maximum[1] = y;
        //    coord.maximum[2] = z;
        //}

        //public void SetAbsCoords(float x, float y, float z)
        //{
        //    coord.pos_abs[0] = x;
        //    coord.pos_abs[1] = y;
        //    coord.pos_abs[2] = z;
        //}

        //// вращение 3D модели
        //public int RotateModel(int os, float target, float step)
        //{
        //    if ((coord.rotating_angles[os] - target) > 0)
        //    {
        //        coord.rotating_angles[os] -= step;

        //        if (coord.rotating_angles[os] < target)
        //        {
        //            coord.rotating_angles[os] = target;
        //            return -1;
        //        }
        //    }
        //    else
        //    {
        //        coord.rotating_angles[os] += step;

        //        if (coord.rotating_angles[os] > target)
        //        {
        //            coord.rotating_angles[os] = target;
        //            return -1;
        //        }
        //    }
        //    return 0;
        //}

        //// перемещение модели
        //public int MoveModel(int os, float target, float step)
        //{

        //    if (step == 0)
        //        return -1;

        //    float real_target = target;


        //    if ((coord.pos_abs[os] - real_target) > 0)
        //    {
        //        if (coord.pos_abs[os] - step >= coord.minimum[os])
        //        {
        //            coord.pos_abs[os] -= step;

        //            if (coord.pos_abs[os] < real_target)
        //            {
        //                coord.pos_abs[os] = real_target;
        //                return -1;
        //            }

        //            return 0;
        //        }
        //        else
        //        {
        //            coord.pos_abs[os] = coord.minimum[os];
        //            return -1;
        //        }

        //    }
        //    if ((coord.pos_abs[os] - real_target) < 0)
        //    {
        //        if (coord.pos_abs[os] + step <= coord.maximum[os])
        //        {
        //            coord.pos_abs[os] += step;
        //            if (coord.pos_abs[os] > real_target)
        //            {
        //                coord.pos_abs[os] = real_target;
        //                return -1;
        //            }
        //            return 0;
        //        }
        //        else
        //        {
        //            coord.pos_abs[os] = coord.maximum[os];
        //            return -1;
        //        }
        //    }
        //    if ((coord.pos_abs[os] - real_target) == 0)
        //        return -1;

        //    return 0;
        //}
        //#endregion
    }
            
    public class Model_Prop
    {
        public Model_Prop()
        {
            //pos_abs[0] = 0;
            //pos_abs[1] = 0;
            //pos_abs[2] = 0;

            //maximum[0] = 0;
            //maximum[1] = 0;
            //maximum[2] = 0;

            //minimum[0] = 0;
            //minimum[1] = 0;
            //minimum[2] = 0;

            //rotating_angles[0] = 0;
            //rotating_angles[1] = 0;
            //rotating_angles[2] = 0;
        }

        public float[] pos_abs = new float[3];
        public float[] maximum = new float[3];
        public float[] minimum = new float[3];
        public float[] rotating_angles = new float[3];
    }

    public class ModelManager
    {
        private List<Model> modelList;
        public bool isLoad = false;

        public ModelManager()
        {
            modelList = new List<Model>();
        }
        
        public void LoadModel()
        {
            var loader = new ImportModel();
            var path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Models";

            var fileURLs = new List<string>(){ path + @"\body.ase", 
                                                 path + @"\head.ase" };

            ModelTransferBuffer result;
            for (int i = 0; i < fileURLs.Count; i++)
            {               
                result = loader.LoadModel(fileURLs[i]);
                if (result != null)
                {
                    isLoad = true;
                    int num_l = Gl.glGenLists(i + 1);
                    modelList.Add(new Model
                    {
                        IsLoad = true,
                        Object = result.Objects.FirstOrDefault(),
                        Textures = result.Textures,
                        ListID = num_l
                    });
                   
                    Gl.glNewList(num_l, Gl.GL_COMPILE);
                    // отрисовываем геометрию
                    CreateList(modelList.Last());
                    // завершаем дисплейный список
                    Gl.glEndList();
                }          
            }            
        }

        private void CreateList(Model modelList )
        {
            // сохраняем тек матрицу
            Gl.glPushMatrix();

            var element = modelList.Object;
            // если текстура необходима
            if (element.ModelHasTexture)
                if (modelList.Textures[element.GetTextureNum()] != null) // текстурный объект существует
                {
                    Gl.glEnable(Gl.GL_TEXTURE_2D); // включаем режим текстурирования

                    // ID текстуры в памяти
                    uint nn = modelList.Textures[element.GetTextureNum()].GetTextureObj();
                    // активируем (привязываем) эту текстуру
                    Gl.glBindTexture(Gl.GL_TEXTURE_2D, nn);
                }

            Gl.glEnable(Gl.GL_NORMALIZE);

            // начинаем отрисовку полигонов
            Gl.glBegin(Gl.GL_TRIANGLES);

            // по всем полигонам
            for (int i = 0; i < element.VandF[1]; i++)
            {
                // временные переменные, чтобы код был более понятен
                float x1, x2, x3, y1, y2, y3, z1, z2, z3 = 0;

                // вытакскиваем координаты треугольника (полигона)
                x1 = element.vert[0, element.face[0, i]];
                x2 = element.vert[0, element.face[1, i]];
                x3 = element.vert[0, element.face[2, i]];
                y1 = element.vert[1, element.face[0, i]];
                y2 = element.vert[1, element.face[1, i]];
                y3 = element.vert[1, element.face[2, i]];
                z1 = element.vert[2, element.face[0, i]];
                z2 = element.vert[2, element.face[1, i]];
                z3 = element.vert[2, element.face[2, i]];

                // рассчитываем номраль
                float n1 = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
                float n2 = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
                float n3 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

                // устанавливаем номраль
                Gl.glNormal3f(n1, n2, n3);

                // если установлена текстура
                if (element.ModelHasTexture && (element.t_vert != null) && (element.t_face != null))
                {
                    // устанавливаем текстурные координаты для каждой вершины, ну и сами вершины
                    Gl.glTexCoord2f(element.t_vert[0, element.t_face[0, i]], element.t_vert[1, element.t_face[0, i]]);
                    Gl.glVertex3f(x1, y1, z1);

                    Gl.glTexCoord2f(element.t_vert[0, element.t_face[1, i]], element.t_vert[1, element.t_face[1, i]]);
                    Gl.glVertex3f(x2, y2, z2);

                    Gl.glTexCoord2f(element.t_vert[0, element.t_face[2, i]], element.t_vert[1, element.t_face[2, i]]);
                    Gl.glVertex3f(x3, y3, z3);
                }
                else // иначе - отрисовка только вершин
                {
                    Gl.glVertex3f(x1, y1, z1);
                    Gl.glVertex3f(x2, y2, z2);
                    Gl.glVertex3f(x3, y3, z3);
                }
            }

            // завершаем отрисовку
            Gl.glEnd();
            Gl.glDisable(Gl.GL_NORMALIZE);

            // открлючаем текстурирование
            Gl.glDisable(Gl.GL_TEXTURE_2D);       

            // возвращаем сохраненную ранее матрицу
            Gl.glPopMatrix();        
    }
        
        int angle = 0;
        public void DrawModels()
        {
            //сохраняем матрицу
            Gl.glPushMatrix();

             //масштабирование по умолчанию
            Gl.glScalef(0.01f, 0.01f, 0.01f);
            Gl.glTranslated(modelList[0].coord.X, modelList[0].coord.Y, modelList[0].coord.Z);
            modelList[0].coord.Z += (float) 0.3;

            Gl.glCallList(modelList[0].ListID);

           // Gl.glTranslated(0, 0, -5);
            Gl.glRotated(angle++, 0, 0, 1);

            Gl.glCallList(modelList[1].ListID);
            Gl.glPopMatrix();
        }           
    }
}