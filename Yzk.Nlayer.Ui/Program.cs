﻿// See https://aka.ms/new-console-template for more information
//


using Yzk.Nllayer.BusinessLogic.Serives;

Console.WriteLine("Hello, World!");
Bl_Blog bl_Blog = new Bl_Blog();
var lst = bl_Blog.GetBlogs();

Console.WriteLine(lst);