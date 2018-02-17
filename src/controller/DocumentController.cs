﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSDView.model;
using LSDView.view;

namespace LSDView.controller
{
    public class DocumentController
    {
        public ILSDView View { get; set; }

        public IDocument Document { get; private set; }

        private OutlineController _outlineController;
        private ExportController _exportController;

        public DocumentController(ILSDView view, OutlineController outlineController, ExportController exportController)
        {
            View = view;
            _outlineController = outlineController;
            _exportController = exportController;
        }

        public void LoadDocument(IDocument doc, string rootName)
        {
            Document?.OnUnload(this, EventArgs.Empty);

            _exportController.SetExportButtonsEnabled(true);

            Document = doc;
            Document.OnLoad += (sender, args) => _outlineController.PopulateOutlineWithDocument(Document, rootName);
            Document.OnUnload += (sender, args) => _outlineController.ClearOutline();

            Document.OnLoad(this, EventArgs.Empty);
        }
    }
}