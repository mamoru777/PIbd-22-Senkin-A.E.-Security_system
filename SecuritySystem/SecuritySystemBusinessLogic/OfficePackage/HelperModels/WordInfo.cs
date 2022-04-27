﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecuritySystemContracts.ViewModels;

namespace SecuritySystemBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<SecureViewModel> Secures { get; set; }

    }
}
