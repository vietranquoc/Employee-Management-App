﻿#pragma checksum "..\..\..\DepartmentWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C464B488D72086F784A0292BF16968CCF7DC4B9F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WPFApp;


namespace WPFApp {
    
    
    /// <summary>
    /// DepartmentWindow
    /// </summary>
    public partial class DepartmentWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDepartmentId;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDepartmentName;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboManagerId;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboLocationId;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSeachText;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboSeachManager;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboSeachLocation;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgData;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreate;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdate;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\DepartmentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WPFApp;component/departmentwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DepartmentWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\DepartmentWindow.xaml"
            ((WPFApp.DepartmentWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtDepartmentId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtDepartmentName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cboManagerId = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.cboLocationId = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.txtSeachText = ((System.Windows.Controls.TextBox)(target));
            
            #line 54 "..\..\..\DepartmentWindow.xaml"
            this.txtSeachText.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtSeachText_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cboSeachManager = ((System.Windows.Controls.ComboBox)(target));
            
            #line 57 "..\..\..\DepartmentWindow.xaml"
            this.cboSeachManager.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cboSeachManager_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cboSeachLocation = ((System.Windows.Controls.ComboBox)(target));
            
            #line 60 "..\..\..\DepartmentWindow.xaml"
            this.cboSeachLocation.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cboSeachLocation_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dgData = ((System.Windows.Controls.DataGrid)(target));
            
            #line 64 "..\..\..\DepartmentWindow.xaml"
            this.dgData.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgData_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnCreate = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\..\DepartmentWindow.xaml"
            this.btnCreate.Click += new System.Windows.RoutedEventHandler(this.btnCreate_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\..\DepartmentWindow.xaml"
            this.btnUpdate.Click += new System.Windows.RoutedEventHandler(this.btnUpdate_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 81 "..\..\..\DepartmentWindow.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 84 "..\..\..\DepartmentWindow.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

