﻿#pragma checksum "..\..\AddBookWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3D7785A9799E5FBDD743EF1B41690C5FB8C06873FBC4618C19431574DD5567F9"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using C_Work;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace C_Work {
    
    
    /// <summary>
    /// AddBookWindow
    /// </summary>
    public partial class AddBookWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameTB;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox priceTB;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox codeTB;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox autorTB;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pagesCountTB;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addBtn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\AddBookWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/C_Work;component/addbookwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddBookWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\AddBookWindow.xaml"
            ((C_Work.AddBookWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.nameTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.priceTB = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\AddBookWindow.xaml"
            this.priceTB.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.priceTB_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 16 "..\..\AddBookWindow.xaml"
            this.priceTB.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.priceTB_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.codeTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.autorTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.pagesCountTB = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\AddBookWindow.xaml"
            this.pagesCountTB.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.pagesCountTB_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 19 "..\..\AddBookWindow.xaml"
            this.pagesCountTB.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.pagesCountTB_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.addBtn = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\AddBookWindow.xaml"
            this.addBtn.Click += new System.Windows.RoutedEventHandler(this.addBtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cancelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\AddBookWindow.xaml"
            this.cancelBtn.Click += new System.Windows.RoutedEventHandler(this.cancelBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

