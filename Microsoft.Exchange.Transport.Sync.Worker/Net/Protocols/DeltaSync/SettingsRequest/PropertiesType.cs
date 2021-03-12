using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000121 RID: 289
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[XmlType(TypeName = "PropertiesType", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class PropertiesType
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001BBC5 File Offset: 0x00019DC5
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0001BBCD File Offset: 0x00019DCD
		[XmlIgnore]
		public AccountStatusType AccountStatus
		{
			get
			{
				return this.internalAccountStatus;
			}
			set
			{
				this.internalAccountStatus = value;
				this.internalAccountStatusSpecified = true;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001BBDD File Offset: 0x00019DDD
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0001BBE5 File Offset: 0x00019DE5
		[XmlIgnore]
		public ParentalControlStatusType ParentalControlStatus
		{
			get
			{
				return this.internalParentalControlStatus;
			}
			set
			{
				this.internalParentalControlStatus = value;
				this.internalParentalControlStatusSpecified = true;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0001BBF5 File Offset: 0x00019DF5
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0001BBFD File Offset: 0x00019DFD
		[XmlIgnore]
		public long MailBoxSize
		{
			get
			{
				return this.internalMailBoxSize;
			}
			set
			{
				this.internalMailBoxSize = value;
				this.internalMailBoxSizeSpecified = true;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001BC0D File Offset: 0x00019E0D
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0001BC15 File Offset: 0x00019E15
		[XmlIgnore]
		public long MaxMailBoxSize
		{
			get
			{
				return this.internalMaxMailBoxSize;
			}
			set
			{
				this.internalMaxMailBoxSize = value;
				this.internalMaxMailBoxSizeSpecified = true;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001BC25 File Offset: 0x00019E25
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0001BC2D File Offset: 0x00019E2D
		[XmlIgnore]
		public int MaxAttachments
		{
			get
			{
				return this.internalMaxAttachments;
			}
			set
			{
				this.internalMaxAttachments = value;
				this.internalMaxAttachmentsSpecified = true;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001BC3D File Offset: 0x00019E3D
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0001BC45 File Offset: 0x00019E45
		[XmlIgnore]
		public long MaxMessageSize
		{
			get
			{
				return this.internalMaxMessageSize;
			}
			set
			{
				this.internalMaxMessageSize = value;
				this.internalMaxMessageSizeSpecified = true;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0001BC55 File Offset: 0x00019E55
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0001BC5D File Offset: 0x00019E5D
		[XmlIgnore]
		public long MaxUnencodedMessageSize
		{
			get
			{
				return this.internalMaxUnencodedMessageSize;
			}
			set
			{
				this.internalMaxUnencodedMessageSize = value;
				this.internalMaxUnencodedMessageSizeSpecified = true;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001BC6D File Offset: 0x00019E6D
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0001BC75 File Offset: 0x00019E75
		[XmlIgnore]
		public int MaxFilters
		{
			get
			{
				return this.internalMaxFilters;
			}
			set
			{
				this.internalMaxFilters = value;
				this.internalMaxFiltersSpecified = true;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0001BC85 File Offset: 0x00019E85
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0001BC8D File Offset: 0x00019E8D
		[XmlIgnore]
		public int MaxFilterClauseValueLength
		{
			get
			{
				return this.internalMaxFilterClauseValueLength;
			}
			set
			{
				this.internalMaxFilterClauseValueLength = value;
				this.internalMaxFilterClauseValueLengthSpecified = true;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0001BC9D File Offset: 0x00019E9D
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0001BCA5 File Offset: 0x00019EA5
		[XmlIgnore]
		public int MaxRecipients
		{
			get
			{
				return this.internalMaxRecipients;
			}
			set
			{
				this.internalMaxRecipients = value;
				this.internalMaxRecipientsSpecified = true;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001BCB5 File Offset: 0x00019EB5
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0001BCBD File Offset: 0x00019EBD
		[XmlIgnore]
		public int MaxUserSignatureLength
		{
			get
			{
				return this.internalMaxUserSignatureLength;
			}
			set
			{
				this.internalMaxUserSignatureLength = value;
				this.internalMaxUserSignatureLengthSpecified = true;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001BCCD File Offset: 0x00019ECD
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0001BCD5 File Offset: 0x00019ED5
		[XmlIgnore]
		public int BlockListAddressMax
		{
			get
			{
				return this.internalBlockListAddressMax;
			}
			set
			{
				this.internalBlockListAddressMax = value;
				this.internalBlockListAddressMaxSpecified = true;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001BCE5 File Offset: 0x00019EE5
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0001BCED File Offset: 0x00019EED
		[XmlIgnore]
		public int BlockListDomainMax
		{
			get
			{
				return this.internalBlockListDomainMax;
			}
			set
			{
				this.internalBlockListDomainMax = value;
				this.internalBlockListDomainMaxSpecified = true;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001BCFD File Offset: 0x00019EFD
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0001BD05 File Offset: 0x00019F05
		[XmlIgnore]
		public int WhiteListAddressMax
		{
			get
			{
				return this.internalWhiteListAddressMax;
			}
			set
			{
				this.internalWhiteListAddressMax = value;
				this.internalWhiteListAddressMaxSpecified = true;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001BD15 File Offset: 0x00019F15
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0001BD1D File Offset: 0x00019F1D
		[XmlIgnore]
		public int WhiteListDomainMax
		{
			get
			{
				return this.internalWhiteListDomainMax;
			}
			set
			{
				this.internalWhiteListDomainMax = value;
				this.internalWhiteListDomainMaxSpecified = true;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001BD2D File Offset: 0x00019F2D
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0001BD35 File Offset: 0x00019F35
		[XmlIgnore]
		public int WhiteToListMax
		{
			get
			{
				return this.internalWhiteToListMax;
			}
			set
			{
				this.internalWhiteToListMax = value;
				this.internalWhiteToListMaxSpecified = true;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001BD45 File Offset: 0x00019F45
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0001BD4D File Offset: 0x00019F4D
		[XmlIgnore]
		public int AlternateFromListMax
		{
			get
			{
				return this.internalAlternateFromListMax;
			}
			set
			{
				this.internalAlternateFromListMax = value;
				this.internalAlternateFromListMaxSpecified = true;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001BD5D File Offset: 0x00019F5D
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0001BD65 File Offset: 0x00019F65
		[XmlIgnore]
		public int MaxDailySendMessages
		{
			get
			{
				return this.internalMaxDailySendMessages;
			}
			set
			{
				this.internalMaxDailySendMessages = value;
				this.internalMaxDailySendMessagesSpecified = true;
			}
		}

		// Token: 0x04000493 RID: 1171
		[XmlElement(ElementName = "AccountStatus", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AccountStatusType internalAccountStatus;

		// Token: 0x04000494 RID: 1172
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalAccountStatusSpecified;

		// Token: 0x04000495 RID: 1173
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ParentalControlStatus", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public ParentalControlStatusType internalParentalControlStatus;

		// Token: 0x04000496 RID: 1174
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalParentalControlStatusSpecified;

		// Token: 0x04000497 RID: 1175
		[XmlElement(ElementName = "MailBoxSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "long", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public long internalMailBoxSize;

		// Token: 0x04000498 RID: 1176
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMailBoxSizeSpecified;

		// Token: 0x04000499 RID: 1177
		[XmlElement(ElementName = "MaxMailBoxSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "long", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public long internalMaxMailBoxSize;

		// Token: 0x0400049A RID: 1178
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxMailBoxSizeSpecified;

		// Token: 0x0400049B RID: 1179
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxAttachments", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxAttachments;

		// Token: 0x0400049C RID: 1180
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxAttachmentsSpecified;

		// Token: 0x0400049D RID: 1181
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxMessageSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "long", Namespace = "HMSETTINGS:")]
		public long internalMaxMessageSize;

		// Token: 0x0400049E RID: 1182
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxMessageSizeSpecified;

		// Token: 0x0400049F RID: 1183
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxUnencodedMessageSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "long", Namespace = "HMSETTINGS:")]
		public long internalMaxUnencodedMessageSize;

		// Token: 0x040004A0 RID: 1184
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxUnencodedMessageSizeSpecified;

		// Token: 0x040004A1 RID: 1185
		[XmlElement(ElementName = "MaxFilters", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxFilters;

		// Token: 0x040004A2 RID: 1186
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxFiltersSpecified;

		// Token: 0x040004A3 RID: 1187
		[XmlElement(ElementName = "MaxFilterClauseValueLength", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxFilterClauseValueLength;

		// Token: 0x040004A4 RID: 1188
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxFilterClauseValueLengthSpecified;

		// Token: 0x040004A5 RID: 1189
		[XmlElement(ElementName = "MaxRecipients", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxRecipients;

		// Token: 0x040004A6 RID: 1190
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxRecipientsSpecified;

		// Token: 0x040004A7 RID: 1191
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxUserSignatureLength", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxUserSignatureLength;

		// Token: 0x040004A8 RID: 1192
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxUserSignatureLengthSpecified;

		// Token: 0x040004A9 RID: 1193
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "BlockListAddressMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalBlockListAddressMax;

		// Token: 0x040004AA RID: 1194
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalBlockListAddressMaxSpecified;

		// Token: 0x040004AB RID: 1195
		[XmlElement(ElementName = "BlockListDomainMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalBlockListDomainMax;

		// Token: 0x040004AC RID: 1196
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalBlockListDomainMaxSpecified;

		// Token: 0x040004AD RID: 1197
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "WhiteListAddressMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalWhiteListAddressMax;

		// Token: 0x040004AE RID: 1198
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalWhiteListAddressMaxSpecified;

		// Token: 0x040004AF RID: 1199
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "WhiteListDomainMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalWhiteListDomainMax;

		// Token: 0x040004B0 RID: 1200
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalWhiteListDomainMaxSpecified;

		// Token: 0x040004B1 RID: 1201
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "WhiteToListMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalWhiteToListMax;

		// Token: 0x040004B2 RID: 1202
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalWhiteToListMaxSpecified;

		// Token: 0x040004B3 RID: 1203
		[XmlElement(ElementName = "AlternateFromListMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalAlternateFromListMax;

		// Token: 0x040004B4 RID: 1204
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalAlternateFromListMaxSpecified;

		// Token: 0x040004B5 RID: 1205
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxDailySendMessages", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxDailySendMessages;

		// Token: 0x040004B6 RID: 1206
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxDailySendMessagesSpecified;
	}
}
