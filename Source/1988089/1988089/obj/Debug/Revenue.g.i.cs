﻿#pragma checksum "..\..\Revenue.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BDCEE434949CB656B584307DD3E6248B50F9704A7C8C8FD0E0E9601A2506E84B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LiveCharts.Wpf;
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
using _1988089;


namespace _1988089 {
    
    
    /// <summary>
    /// Revenue
    /// </summary>
    public partial class Revenue : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PurchaseList;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button First;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Previous;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PageInfo;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Next;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Last;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label currentPage;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker fromDtpk;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker toDtpk;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label revenueLabel;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\Revenue.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.PieChart revenueChart;
        
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
            System.Uri resourceLocater = new System.Uri("/1988089;component/revenue.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Revenue.xaml"
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
            
            #line 9 "..\..\Revenue.xaml"
            ((_1988089.Revenue)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PurchaseList = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.First = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\Revenue.xaml"
            this.First.Click += new System.Windows.RoutedEventHandler(this.First_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Previous = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\Revenue.xaml"
            this.Previous.Click += new System.Windows.RoutedEventHandler(this.Previous_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PageInfo = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.Next = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\Revenue.xaml"
            this.Next.Click += new System.Windows.RoutedEventHandler(this.Next_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Last = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\Revenue.xaml"
            this.Last.Click += new System.Windows.RoutedEventHandler(this.Last_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.currentPage = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.fromDtpk = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 10:
            this.toDtpk = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 11:
            
            #line 51 "..\..\Revenue.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.revenueLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            
            #line 54 "..\..\Revenue.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 14:
            this.revenueChart = ((LiveCharts.Wpf.PieChart)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

