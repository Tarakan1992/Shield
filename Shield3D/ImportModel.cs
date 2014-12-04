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
    public class ModelTransferBuffer
    {
        public List<LIMB> Objects { set; get; }
        public List<TexturesForObjects> Textures { set; get; }
        public ModelTransferBuffer()
        {
            Objects = null;
            Textures = null;
        }
    }

    public class ImportModel
    {
        public ImportModel()
        {

        }

        private int GlobalStringFrom = 0;

        public ModelTransferBuffer LoadModel(string FileName)
        {            
            //TexturesForObjects[] texObjects;
            var materialNumber = 0;
            // модель может содержать до 256 под-объектов
            //var limbs = new LIMB[256];
            var resultObjects = new List<LIMB>();
            var texObjects = new List<TexturesForObjects>();
            // счетчик скинут
            int limb_ = -1;

            // имся файла
            //FName = FileName;

            // начинаем чтение файла
            StreamReader sw = File.OpenText(FileName);

            // временные буферы
            string a_buff = "";
            string b_buff = "";
            string c_buff = "";

            // счетчики вершин и полигонов
            int ver = 0, fac = 0;

            // если строка успешно прочитана
            while ((a_buff = sw.ReadLine()) != null)
            {
                // получаем первое слово
                b_buff = GetFirstWord(a_buff, 0);
                if (b_buff[0] == '*') // определеям, является ли первый символ звездочкой
                {
                    switch (b_buff) // если да, то проверяем какое управляющее слово содержится в первом прочитаном слове
                    {
                        //case "*MATERIAL_COUNT": // счетчик материалов
                        //    {
                        //        // получаем первое слово от символа указанного в GlobalStringFrom
                        //        c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                        //        int mat = System.Convert.ToInt32(c_buff);

                        //        // создаем объект для текстуры в памяти
                        //        texObjects = new TexturesForObjects[mat];
                        //        continue;
                        //    }

                        case "*MATERIAL_REF": // номер текстуры
                            {
                                // записываем для текущего под-объекта номер текстуры
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                int mat_ref = System.Convert.ToInt32(c_buff);

                                // устанавливаем номер материала, соответствующий данной модели.
                                resultObjects[limb_].SetMaterialNum(mat_ref);
                                continue;
                            }

                        case "*MATERIAL": // указание на материал
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                materialNumber = System.Convert.ToInt32(c_buff);
                                continue;
                            }

                        case "*GEOMOBJECT": // начинается описание геметрии под-объекта
                            {
                                limb_++; // записываем в счетчик под-объектов
                                //limbs.Add(new LIMB);
                                continue;
                            }

                        case "*MESH_NUMVERTEX": // количесвто вершин в под-объекте
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                ver = System.Convert.ToInt32(c_buff);
                                continue;
                            }

                        case "*BITMAP": // имя текстуры
                            {
                                c_buff = ""; // обнуляем временный буффер

                                for (int ax = GlobalStringFrom + 2; ax < a_buff.Length - 1; ax++)
                                    c_buff += a_buff[ax]; // считываем имя текстуры

                                //texObjects[materialNumber] = new TexturesForObjects(); // новый объект для текстуры
                                texObjects.Add(new TexturesForObjects());

                                texObjects[materialNumber].LoadTextureForModel(c_buff); // загружаем текстуру

                                continue;
                            }

                        case "*MESH_NUMTVERTEX": // количество текстурных координат, данное слово говорит о наличии текстурных координат - следовательно мы должны выделить память для них
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                if (resultObjects[limb_] != null)
                                {
                                    resultObjects[limb_].createTextureVertexMem(System.Convert.ToInt32(c_buff));
                                }
                                continue;
                            }

                        case "*MESH_NUMTVFACES":  // память для текстурных координат (faces)
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);

                                if (resultObjects[limb_] != null)
                                {
                                    // выделяем память для текстурныйх координат
                                    resultObjects[limb_].createTextureFaceMem(System.Convert.ToInt32(c_buff));
                                }
                                continue;
                            }

                        case "*MESH_NUMFACES": // количество полиговов в под-объекте
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                fac = System.Convert.ToInt32(c_buff);

                                // если было объвляющее слово *GEOMOBJECT (гарантия выполнения условия limb_ > -1) и были указаны количство вершин
                                if (limb_ > -1 && ver > -1 && fac > -1)
                                {
                                    // создаем новый под-объект в памяти
                                    //limbs[limb_] = new LIMB(ver, fac);
                                    resultObjects.Add(new LIMB(ver, fac));
                                }
                                else
                                {
                                    // иначе завершаем неудачей
                                    return null;
                                }
                                continue;
                            }

                        case "*MESH_VERTEX": // информация о вершине
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return null;
                                if (resultObjects[limb_] == null)
                                    return null;

                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // полчучаем информацию о кооринатах и номере вершины
                                // (получаем все слова в строке)
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразовываем в целое цисло
                                int NomVertex = System.Convert.ToInt32(a1);

                                // заменяем точки в представлении числа с плавающей точкой, на запятые, чтобы правильно выполнилась функция 
                                // преобразования строки в дробное число
                                a2 = a2.Replace('.', ',');
                                a3 = a3.Replace('.', ',');
                                a4 = a4.Replace('.', ',');

                                // записываем информацию о вершине
                                resultObjects[limb_].vert[0, NomVertex] = (float)System.Convert.ToDouble(a2); // x
                                resultObjects[limb_].vert[1, NomVertex] = (float)System.Convert.ToDouble(a3); // y
                                resultObjects[limb_].vert[2, NomVertex] = (float)System.Convert.ToDouble(a4); // z

                                continue;
                            }

                        case "*MESH_FACE": // информация о полигоне
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return null;
                                if (resultObjects[limb_] == null)
                                    return null;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);
                                a5 = GetFirstWord(a_buff, GlobalStringFrom);
                                a6 = GetFirstWord(a_buff, GlobalStringFrom);
                                a7 = GetFirstWord(a_buff, GlobalStringFrom);

                                // получаем нмоер полигона из первого слова в строке, заменив последний символ ":" после номера на флаг окончания строки.
                                int NomFace = System.Convert.ToInt32(a1.Replace(':', '\0'));

                                // записываем номера вершин, которые нас интересуют
                                resultObjects[limb_].face[0, NomFace] = System.Convert.ToInt32(a3);
                                resultObjects[limb_].face[1, NomFace] = System.Convert.ToInt32(a5);
                                resultObjects[limb_].face[2, NomFace] = System.Convert.ToInt32(a7);

                                continue;

                            }

                        // текстурые координаты
                        case "*MESH_TVERT":
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return null;
                                if (resultObjects[limb_] == null)
                                    return null;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер вершины
                                int NumVertex = System.Convert.ToInt32(a1);

                                // заменяем точки в представлении числа с плавающей точкой, на запятые, чтобы правильно выполнилась функция 
                                // преобразования строки в дробное число
                                a2 = a2.Replace('.', ',');
                                a3 = a3.Replace('.', ',');
                                a4 = a4.Replace('.', ',');

                                // записываем значение вершины
                                resultObjects[limb_].t_vert[0, NumVertex] = (float)System.Convert.ToDouble(a2); // x
                                resultObjects[limb_].t_vert[1, NumVertex] = (float)System.Convert.ToDouble(a3); // y
                                resultObjects[limb_].t_vert[2, NumVertex] = (float)System.Convert.ToDouble(a4); // z

                                continue;
                            }

                        // привязка текстурных координат к полигонам
                        case "*MESH_TFACE":
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return null;
                                if (resultObjects[limb_] == null)
                                    return null;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер полигона
                                int NomFace = System.Convert.ToInt32(a1);

                                // записываем номера вершин, которые опиывают полигон
                                resultObjects[limb_].t_face[0, NomFace] = System.Convert.ToInt32(a2);
                                resultObjects[limb_].t_face[1, NomFace] = System.Convert.ToInt32(a3);
                                resultObjects[limb_].t_face[2, NomFace] = System.Convert.ToInt32(a4);

                                continue;
                            }
                    }
                }
            }
            
            var result = new ModelTransferBuffer();
            result.Objects = new List<LIMB>(resultObjects);
            result.Textures = new List<TexturesForObjects>(texObjects);
            
            return result;








            // пересохраняем количесвто полигонов
            //count_limbs = limb_;

            //// получаем ID для создаваемого дисплейного списка
            //int nom_l = Gl.glGenLists(1);
            //thisList = nom_l;
            //// генерируем новый дисплейный список
            //Gl.glNewList(nom_l, Gl.GL_COMPILE);
            //// отрисовываем геометрию
            //CreateList();
            //// завершаем дисплейный список
            //Gl.glEndList();

            //// загрузка завершена
            //isLoad = true;

            //return 0;
        }

        // функиц я получения первого слова строки
        private string GetFirstWord(string word, int from)
        {
            // from указывает на позицию, начиная с которой будет выполнятся чтение файла
            char a = word[from]; // первый символ
            string res_buff = ""; // временный буффер
            int L = word.Length; // длина слова

            if (word[from] == ' ' || word[from] == '\t') // если первый символ, с которого предстоит искать слово является пробелом или знаком табуляции
            {
                // необходимо вычисслить наличие секции проблеов или знаков табуляции и откинуть их
                int ax = 0;
                // проходим до конца слова
                for (ax = from; ax < L; ax++)
                {
                    a = word[ax];
                    if (a != ' ' && a != '\t') // если встречаем символ пробела или табуляции
                        break; // выходим из цикла. 
                    // таким образом мы откидываем все последовательности пробелов или знаков табуляции, с которых могла начинатся переданная строка
                }

                if (ax == L) // если вся представленная строка является набором пробелов или знаков табуляции - возвращаем res_buff
                    return res_buff;
                else
                    from = ax; // иначе сохраняем значение ax
            }
            int bx = 0;

            // теперь, когда пробелы и табуляция откинуты мы непосредственно вычисляем слово
            for (bx = from; bx < L; bx++)
            {
                // если встретили знак пробела или табуляции - завершаем чтение слова
                if (word[bx] == ' ' || word[bx] == '\t')
                    break;
                // записываем символ в бременный буффер, постепенно получая таким образом слово
                res_buff += word[bx];
            }

            // если дошли до конца строки
            if (bx == L)
                bx--; // убераем посл значение

            GlobalStringFrom = bx; // позиция в данной строке, для чтения следующего слова в данной строке

            return res_buff; // возвращаем слово
        }
    }
}
