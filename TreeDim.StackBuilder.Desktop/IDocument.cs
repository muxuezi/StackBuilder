﻿#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using treeDiM.StackBuilder.Basics;
#endregion

namespace treeDiM.StackBuilder.Desktop
{
    public interface IDocument
    {
        string Name { get; }
        string FilePath { get; }
        bool IsDirty { get; }
        bool IsNew { get; }
        void Save();
        void SaveAs(string context);
        void Close();
        List<IView> Views { get; }
        IView ActiveView { get; }
        void AddView(IView view);
        void RemoveView(IView view);

        void EditAnalysis(Analysis analysis);

        event EventHandler Modified;
    }


}
