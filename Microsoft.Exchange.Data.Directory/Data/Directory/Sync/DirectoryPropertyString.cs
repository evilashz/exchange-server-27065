﻿using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000874 RID: 2164
	[XmlInclude(typeof(DirectoryPropertyStringSingleColor))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To2048))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleServiceInstanceSelectionTagPrefix))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleMailNickname))]
	[XmlInclude(typeof(DirectoryPropertyServicePrincipalName))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To2048))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To1123))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To1024))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To512))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To500))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To454))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To256))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To128))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To64))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To40))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To20))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To16))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To6))]
	[XmlInclude(typeof(DirectoryPropertyStringSingleLength1To3))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To3))]
	[XmlInclude(typeof(DirectoryPropertyStringSingle))]
	[XmlInclude(typeof(DirectoryPropertyTargetAddress))]
	[XmlInclude(typeof(DirectoryPropertyProxyAddresses))]
	[XmlInclude(typeof(DirectoryPropertyStringStsTokenType))]
	[XmlInclude(typeof(DirectoryPropertyStringLength2To500))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To1123))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To1024))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To512))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To256))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To100))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To64))]
	[XmlInclude(typeof(DirectoryPropertyStringLength1To40))]
	[Serializable]
	public abstract class DirectoryPropertyString : DirectoryProperty
	{
		// Token: 0x06006D10 RID: 27920 RVA: 0x0017519F File Offset: 0x0017339F
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D11 RID: 27921 RVA: 0x001751B5 File Offset: 0x001733B5
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new string[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026F6 RID: 9974
		// (get) Token: 0x06006D12 RID: 27922 RVA: 0x001751E5 File Offset: 0x001733E5
		// (set) Token: 0x06006D13 RID: 27923 RVA: 0x001751ED File Offset: 0x001733ED
		[XmlElement("Value", Order = 0)]
		public string[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04004768 RID: 18280
		private string[] valueField;
	}
}
