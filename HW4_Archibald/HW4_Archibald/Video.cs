﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HW4_Archibald
{
    class Video : Search, IMedia<Video>
    {
        Data<Video> dv = new Data<Video>();
              
        string[] fileName, fileExtention, dateLastAccessed;
        int[] index ;

        int Length;
        //public int[] Index { set { index = value; } get { return index; } }
        //public string[] FileName { set { fileName = value; } get { return fileName; } }
        //public string[] FileExtention { set { fileExtention = value; } get { return fileExtention; } }
        //public string[] DateLastAccessed { set { dateLastAccessed = value; } get { return dateLastAccessed; } }

        public void Search(string directory)
        {
            string[] fileTypes = { ".avi", ".mp4" };

            dv.Search(directory, fileTypes);
            //string[] retstr;

            //for (int b = 0; b < fileTypes.Length; b++)
            //{
            //    string[] temp;
            //    int tempLen = 0;
            //    retstr = ReturnFilePath(directory, fileTypes[b]);
            //    temp = retstr;
            //    if (retstr != null)
            //    {
            //        tempLen = retstr.Length;
            //        Array.Resize<string>(ref retstr, retstr.Length + temp.Length);

            //        for (int c = 0; c < temp.Length; c++)
            //        {
            //            if (tempLen != 0)
            //            {
            //                retstr[c + tempLen] = temp[c];
            //            }
            //            else
            //            {
            //                retstr[c] = temp[c];
            //            }
            //        }

            //        RetFileType(temp);
            //        RetFileExtention(temp);

                    //for (int i = 0; i < retstr.Length; i++)
                    //{
                    //    Index[i] = i;
                    //    dv.FileName.Insert(Index[i], FileName[i]);
                    //    dv.FileExtention.Insert(Index[i], FileExtention[i]);
                    //    dv.DateAccessed.Insert(Index[i], DateLastAccessed[i]);
                    //    dv.FileDirectory.Insert(Index[i], retstr[i]);
                    //    dv = new Data<Video>();
                    //    Length = i;
                    //}
                //}
            //}
        }

        public void Remove(int Index)
        {
            dv.RemoveValue(Index);
        }
                    
        public void PrintValues()
        {
            Length = dv.FileName.Length;

            for (int i = 0; i < Length; i++)
            {
                Console.WriteLine($"File Name: {dv.FileName[i]}\n File Extention {dv.FileExtention[i]} \n Date Last accessed {dv.DateLastAccessed[i]}");
                //Console.WriteLine($" Index: {i} \n File name: {FileName} \n File Extention: {FileExtention}\n Date Last Accessed {DateLastAccessed}");
            }
        }

        //public void RetFileType(string[] location)
        //{
        //    for (int i = 0; i < location.Length; i++)
        //    {
        //        dv.DateLastAccessed[i] = Convert.ToString(Directory.GetLastAccessTime(location[i]));

        //        string[] temp;
        //        temp = location[i].Split('/').ToArray<string>();
        //        dv.FileName[i] = temp[temp.Length - 1];

        //    }
        //}

        //public void RetFileExtention(string[] location)
        //{
        //    for (int i = 0; i < location.Length; i++)
        //    {
        //        string[] temp;
        //        temp = location[i].Split('.').ToArray<string>();
        //        dv.FileExtention[i] = temp[temp.Length - 1];
        //    }
        //}
    }
}