using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200084C RID: 2124
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class Device : DirectoryObject
	{
		// Token: 0x06006A34 RID: 27188 RVA: 0x00172799 File Offset: 0x00170999
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
		}

		// Token: 0x170025BB RID: 9659
		// (get) Token: 0x06006A35 RID: 27189 RVA: 0x0017279B File Offset: 0x0017099B
		// (set) Token: 0x06006A36 RID: 27190 RVA: 0x001727A3 File Offset: 0x001709A3
		[XmlElement(Order = 0)]
		public DirectoryPropertyBooleanSingle AccountEnabled
		{
			get
			{
				return this.accountEnabledField;
			}
			set
			{
				this.accountEnabledField = value;
			}
		}

		// Token: 0x170025BC RID: 9660
		// (get) Token: 0x06006A37 RID: 27191 RVA: 0x001727AC File Offset: 0x001709AC
		// (set) Token: 0x06006A38 RID: 27192 RVA: 0x001727B4 File Offset: 0x001709B4
		[XmlElement(Order = 1)]
		public DirectoryPropertyXmlAlternativeSecurityId AlternativeSecurityId
		{
			get
			{
				return this.alternativeSecurityIdField;
			}
			set
			{
				this.alternativeSecurityIdField = value;
			}
		}

		// Token: 0x170025BD RID: 9661
		// (get) Token: 0x06006A39 RID: 27193 RVA: 0x001727BD File Offset: 0x001709BD
		// (set) Token: 0x06006A3A RID: 27194 RVA: 0x001727C5 File Offset: 0x001709C5
		[XmlElement(Order = 2)]
		public DirectoryPropertyDateTimeSingle ApproximateLastLogonTimestamp
		{
			get
			{
				return this.approximateLastLogonTimestampField;
			}
			set
			{
				this.approximateLastLogonTimestampField = value;
			}
		}

		// Token: 0x170025BE RID: 9662
		// (get) Token: 0x06006A3B RID: 27195 RVA: 0x001727CE File Offset: 0x001709CE
		// (set) Token: 0x06006A3C RID: 27196 RVA: 0x001727D6 File Offset: 0x001709D6
		[XmlElement(Order = 3)]
		public DirectoryPropertyGuidSingle DeviceId
		{
			get
			{
				return this.deviceIdField;
			}
			set
			{
				this.deviceIdField = value;
			}
		}

		// Token: 0x170025BF RID: 9663
		// (get) Token: 0x06006A3D RID: 27197 RVA: 0x001727DF File Offset: 0x001709DF
		// (set) Token: 0x06006A3E RID: 27198 RVA: 0x001727E7 File Offset: 0x001709E7
		[XmlElement(Order = 4)]
		public DirectoryPropertyInt32Single DeviceObjectVersion
		{
			get
			{
				return this.deviceObjectVersionField;
			}
			set
			{
				this.deviceObjectVersionField = value;
			}
		}

		// Token: 0x170025C0 RID: 9664
		// (get) Token: 0x06006A3F RID: 27199 RVA: 0x001727F0 File Offset: 0x001709F0
		// (set) Token: 0x06006A40 RID: 27200 RVA: 0x001727F8 File Offset: 0x001709F8
		[XmlElement(Order = 5)]
		public DirectoryPropertyStringSingleLength1To1024 DeviceOSType
		{
			get
			{
				return this.deviceOSTypeField;
			}
			set
			{
				this.deviceOSTypeField = value;
			}
		}

		// Token: 0x170025C1 RID: 9665
		// (get) Token: 0x06006A41 RID: 27201 RVA: 0x00172801 File Offset: 0x00170A01
		// (set) Token: 0x06006A42 RID: 27202 RVA: 0x00172809 File Offset: 0x00170A09
		[XmlElement(Order = 6)]
		public DirectoryPropertyStringSingleLength1To512 DeviceOSVersion
		{
			get
			{
				return this.deviceOSVersionField;
			}
			set
			{
				this.deviceOSVersionField = value;
			}
		}

		// Token: 0x170025C2 RID: 9666
		// (get) Token: 0x06006A43 RID: 27203 RVA: 0x00172812 File Offset: 0x00170A12
		// (set) Token: 0x06006A44 RID: 27204 RVA: 0x0017281A File Offset: 0x00170A1A
		[XmlElement(Order = 7)]
		public DirectoryPropertyStringLength1To1024 DevicePhysicalIds
		{
			get
			{
				return this.devicePhysicalIdsField;
			}
			set
			{
				this.devicePhysicalIdsField = value;
			}
		}

		// Token: 0x170025C3 RID: 9667
		// (get) Token: 0x06006A45 RID: 27205 RVA: 0x00172823 File Offset: 0x00170A23
		// (set) Token: 0x06006A46 RID: 27206 RVA: 0x0017282B File Offset: 0x00170A2B
		[XmlElement(Order = 8)]
		public DirectoryPropertyBooleanSingle DirSyncEnabled
		{
			get
			{
				return this.dirSyncEnabledField;
			}
			set
			{
				this.dirSyncEnabledField = value;
			}
		}

		// Token: 0x170025C4 RID: 9668
		// (get) Token: 0x06006A47 RID: 27207 RVA: 0x00172834 File Offset: 0x00170A34
		// (set) Token: 0x06006A48 RID: 27208 RVA: 0x0017283C File Offset: 0x00170A3C
		[XmlElement(Order = 9)]
		public DirectoryPropertyStringSingleLength1To256 DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x170025C5 RID: 9669
		// (get) Token: 0x06006A49 RID: 27209 RVA: 0x00172845 File Offset: 0x00170A45
		// (set) Token: 0x06006A4A RID: 27210 RVA: 0x0017284D File Offset: 0x00170A4D
		[XmlElement(Order = 10)]
		public DirectoryPropertyStringLength1To100 ExchangeActiveSyncId
		{
			get
			{
				return this.exchangeActiveSyncIdField;
			}
			set
			{
				this.exchangeActiveSyncIdField = value;
			}
		}

		// Token: 0x170025C6 RID: 9670
		// (get) Token: 0x06006A4B RID: 27211 RVA: 0x00172856 File Offset: 0x00170A56
		// (set) Token: 0x06006A4C RID: 27212 RVA: 0x0017285E File Offset: 0x00170A5E
		[XmlElement(Order = 11)]
		public DirectoryPropertyBooleanSingle IsCompliant
		{
			get
			{
				return this.isCompliantField;
			}
			set
			{
				this.isCompliantField = value;
			}
		}

		// Token: 0x170025C7 RID: 9671
		// (get) Token: 0x06006A4D RID: 27213 RVA: 0x00172867 File Offset: 0x00170A67
		// (set) Token: 0x06006A4E RID: 27214 RVA: 0x0017286F File Offset: 0x00170A6F
		[XmlElement(Order = 12)]
		public DirectoryPropertyBooleanSingle IsManaged
		{
			get
			{
				return this.isManagedField;
			}
			set
			{
				this.isManagedField = value;
			}
		}

		// Token: 0x170025C8 RID: 9672
		// (get) Token: 0x06006A4F RID: 27215 RVA: 0x00172878 File Offset: 0x00170A78
		// (set) Token: 0x06006A50 RID: 27216 RVA: 0x00172880 File Offset: 0x00170A80
		[XmlElement(Order = 13)]
		public DirectoryPropertyStringSingleLength1To256 SourceAnchor
		{
			get
			{
				return this.sourceAnchorField;
			}
			set
			{
				this.sourceAnchorField = value;
			}
		}

		// Token: 0x170025C9 RID: 9673
		// (get) Token: 0x06006A51 RID: 27217 RVA: 0x00172889 File Offset: 0x00170A89
		// (set) Token: 0x06006A52 RID: 27218 RVA: 0x00172891 File Offset: 0x00170A91
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x04004580 RID: 17792
		private DirectoryPropertyBooleanSingle accountEnabledField;

		// Token: 0x04004581 RID: 17793
		private DirectoryPropertyXmlAlternativeSecurityId alternativeSecurityIdField;

		// Token: 0x04004582 RID: 17794
		private DirectoryPropertyDateTimeSingle approximateLastLogonTimestampField;

		// Token: 0x04004583 RID: 17795
		private DirectoryPropertyGuidSingle deviceIdField;

		// Token: 0x04004584 RID: 17796
		private DirectoryPropertyInt32Single deviceObjectVersionField;

		// Token: 0x04004585 RID: 17797
		private DirectoryPropertyStringSingleLength1To1024 deviceOSTypeField;

		// Token: 0x04004586 RID: 17798
		private DirectoryPropertyStringSingleLength1To512 deviceOSVersionField;

		// Token: 0x04004587 RID: 17799
		private DirectoryPropertyStringLength1To1024 devicePhysicalIdsField;

		// Token: 0x04004588 RID: 17800
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x04004589 RID: 17801
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x0400458A RID: 17802
		private DirectoryPropertyStringLength1To100 exchangeActiveSyncIdField;

		// Token: 0x0400458B RID: 17803
		private DirectoryPropertyBooleanSingle isCompliantField;

		// Token: 0x0400458C RID: 17804
		private DirectoryPropertyBooleanSingle isManagedField;

		// Token: 0x0400458D RID: 17805
		private DirectoryPropertyStringSingleLength1To256 sourceAnchorField;

		// Token: 0x0400458E RID: 17806
		private XmlAttribute[] anyAttrField;
	}
}
