using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000187 RID: 391
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class RightsManagementLicenseDataType
	{
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00023DFB File Offset: 0x00021FFB
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x00023E03 File Offset: 0x00022003
		public int RightsManagedMessageDecryptionStatus
		{
			get
			{
				return this.rightsManagedMessageDecryptionStatusField;
			}
			set
			{
				this.rightsManagedMessageDecryptionStatusField = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00023E0C File Offset: 0x0002200C
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x00023E14 File Offset: 0x00022014
		[XmlIgnore]
		public bool RightsManagedMessageDecryptionStatusSpecified
		{
			get
			{
				return this.rightsManagedMessageDecryptionStatusFieldSpecified;
			}
			set
			{
				this.rightsManagedMessageDecryptionStatusFieldSpecified = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00023E1D File Offset: 0x0002201D
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x00023E25 File Offset: 0x00022025
		public string RmsTemplateId
		{
			get
			{
				return this.rmsTemplateIdField;
			}
			set
			{
				this.rmsTemplateIdField = value;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00023E2E File Offset: 0x0002202E
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x00023E36 File Offset: 0x00022036
		public string TemplateName
		{
			get
			{
				return this.templateNameField;
			}
			set
			{
				this.templateNameField = value;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00023E3F File Offset: 0x0002203F
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x00023E47 File Offset: 0x00022047
		public string TemplateDescription
		{
			get
			{
				return this.templateDescriptionField;
			}
			set
			{
				this.templateDescriptionField = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00023E50 File Offset: 0x00022050
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00023E58 File Offset: 0x00022058
		public bool EditAllowed
		{
			get
			{
				return this.editAllowedField;
			}
			set
			{
				this.editAllowedField = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00023E61 File Offset: 0x00022061
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00023E69 File Offset: 0x00022069
		[XmlIgnore]
		public bool EditAllowedSpecified
		{
			get
			{
				return this.editAllowedFieldSpecified;
			}
			set
			{
				this.editAllowedFieldSpecified = value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00023E72 File Offset: 0x00022072
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00023E7A File Offset: 0x0002207A
		public bool ReplyAllowed
		{
			get
			{
				return this.replyAllowedField;
			}
			set
			{
				this.replyAllowedField = value;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00023E83 File Offset: 0x00022083
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x00023E8B File Offset: 0x0002208B
		[XmlIgnore]
		public bool ReplyAllowedSpecified
		{
			get
			{
				return this.replyAllowedFieldSpecified;
			}
			set
			{
				this.replyAllowedFieldSpecified = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x00023E94 File Offset: 0x00022094
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x00023E9C File Offset: 0x0002209C
		public bool ReplyAllAllowed
		{
			get
			{
				return this.replyAllAllowedField;
			}
			set
			{
				this.replyAllAllowedField = value;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00023EA5 File Offset: 0x000220A5
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x00023EAD File Offset: 0x000220AD
		[XmlIgnore]
		public bool ReplyAllAllowedSpecified
		{
			get
			{
				return this.replyAllAllowedFieldSpecified;
			}
			set
			{
				this.replyAllAllowedFieldSpecified = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00023EB6 File Offset: 0x000220B6
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00023EBE File Offset: 0x000220BE
		public bool ForwardAllowed
		{
			get
			{
				return this.forwardAllowedField;
			}
			set
			{
				this.forwardAllowedField = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00023EC7 File Offset: 0x000220C7
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x00023ECF File Offset: 0x000220CF
		[XmlIgnore]
		public bool ForwardAllowedSpecified
		{
			get
			{
				return this.forwardAllowedFieldSpecified;
			}
			set
			{
				this.forwardAllowedFieldSpecified = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x00023ED8 File Offset: 0x000220D8
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x00023EE0 File Offset: 0x000220E0
		public bool ModifyRecipientsAllowed
		{
			get
			{
				return this.modifyRecipientsAllowedField;
			}
			set
			{
				this.modifyRecipientsAllowedField = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00023EE9 File Offset: 0x000220E9
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00023EF1 File Offset: 0x000220F1
		[XmlIgnore]
		public bool ModifyRecipientsAllowedSpecified
		{
			get
			{
				return this.modifyRecipientsAllowedFieldSpecified;
			}
			set
			{
				this.modifyRecipientsAllowedFieldSpecified = value;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00023EFA File Offset: 0x000220FA
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00023F02 File Offset: 0x00022102
		public bool ExtractAllowed
		{
			get
			{
				return this.extractAllowedField;
			}
			set
			{
				this.extractAllowedField = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00023F0B File Offset: 0x0002210B
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x00023F13 File Offset: 0x00022113
		[XmlIgnore]
		public bool ExtractAllowedSpecified
		{
			get
			{
				return this.extractAllowedFieldSpecified;
			}
			set
			{
				this.extractAllowedFieldSpecified = value;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00023F1C File Offset: 0x0002211C
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x00023F24 File Offset: 0x00022124
		public bool PrintAllowed
		{
			get
			{
				return this.printAllowedField;
			}
			set
			{
				this.printAllowedField = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00023F2D File Offset: 0x0002212D
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x00023F35 File Offset: 0x00022135
		[XmlIgnore]
		public bool PrintAllowedSpecified
		{
			get
			{
				return this.printAllowedFieldSpecified;
			}
			set
			{
				this.printAllowedFieldSpecified = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00023F3E File Offset: 0x0002213E
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x00023F46 File Offset: 0x00022146
		public bool ExportAllowed
		{
			get
			{
				return this.exportAllowedField;
			}
			set
			{
				this.exportAllowedField = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x00023F4F File Offset: 0x0002214F
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x00023F57 File Offset: 0x00022157
		[XmlIgnore]
		public bool ExportAllowedSpecified
		{
			get
			{
				return this.exportAllowedFieldSpecified;
			}
			set
			{
				this.exportAllowedFieldSpecified = value;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00023F60 File Offset: 0x00022160
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x00023F68 File Offset: 0x00022168
		public bool ProgrammaticAccessAllowed
		{
			get
			{
				return this.programmaticAccessAllowedField;
			}
			set
			{
				this.programmaticAccessAllowedField = value;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00023F71 File Offset: 0x00022171
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x00023F79 File Offset: 0x00022179
		[XmlIgnore]
		public bool ProgrammaticAccessAllowedSpecified
		{
			get
			{
				return this.programmaticAccessAllowedFieldSpecified;
			}
			set
			{
				this.programmaticAccessAllowedFieldSpecified = value;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x00023F82 File Offset: 0x00022182
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x00023F8A File Offset: 0x0002218A
		public bool IsOwner
		{
			get
			{
				return this.isOwnerField;
			}
			set
			{
				this.isOwnerField = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00023F93 File Offset: 0x00022193
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x00023F9B File Offset: 0x0002219B
		[XmlIgnore]
		public bool IsOwnerSpecified
		{
			get
			{
				return this.isOwnerFieldSpecified;
			}
			set
			{
				this.isOwnerFieldSpecified = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x00023FA4 File Offset: 0x000221A4
		// (set) Token: 0x0600110C RID: 4364 RVA: 0x00023FAC File Offset: 0x000221AC
		public string ContentOwner
		{
			get
			{
				return this.contentOwnerField;
			}
			set
			{
				this.contentOwnerField = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00023FB5 File Offset: 0x000221B5
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x00023FBD File Offset: 0x000221BD
		public string ContentExpiryDate
		{
			get
			{
				return this.contentExpiryDateField;
			}
			set
			{
				this.contentExpiryDateField = value;
			}
		}

		// Token: 0x04000B71 RID: 2929
		private int rightsManagedMessageDecryptionStatusField;

		// Token: 0x04000B72 RID: 2930
		private bool rightsManagedMessageDecryptionStatusFieldSpecified;

		// Token: 0x04000B73 RID: 2931
		private string rmsTemplateIdField;

		// Token: 0x04000B74 RID: 2932
		private string templateNameField;

		// Token: 0x04000B75 RID: 2933
		private string templateDescriptionField;

		// Token: 0x04000B76 RID: 2934
		private bool editAllowedField;

		// Token: 0x04000B77 RID: 2935
		private bool editAllowedFieldSpecified;

		// Token: 0x04000B78 RID: 2936
		private bool replyAllowedField;

		// Token: 0x04000B79 RID: 2937
		private bool replyAllowedFieldSpecified;

		// Token: 0x04000B7A RID: 2938
		private bool replyAllAllowedField;

		// Token: 0x04000B7B RID: 2939
		private bool replyAllAllowedFieldSpecified;

		// Token: 0x04000B7C RID: 2940
		private bool forwardAllowedField;

		// Token: 0x04000B7D RID: 2941
		private bool forwardAllowedFieldSpecified;

		// Token: 0x04000B7E RID: 2942
		private bool modifyRecipientsAllowedField;

		// Token: 0x04000B7F RID: 2943
		private bool modifyRecipientsAllowedFieldSpecified;

		// Token: 0x04000B80 RID: 2944
		private bool extractAllowedField;

		// Token: 0x04000B81 RID: 2945
		private bool extractAllowedFieldSpecified;

		// Token: 0x04000B82 RID: 2946
		private bool printAllowedField;

		// Token: 0x04000B83 RID: 2947
		private bool printAllowedFieldSpecified;

		// Token: 0x04000B84 RID: 2948
		private bool exportAllowedField;

		// Token: 0x04000B85 RID: 2949
		private bool exportAllowedFieldSpecified;

		// Token: 0x04000B86 RID: 2950
		private bool programmaticAccessAllowedField;

		// Token: 0x04000B87 RID: 2951
		private bool programmaticAccessAllowedFieldSpecified;

		// Token: 0x04000B88 RID: 2952
		private bool isOwnerField;

		// Token: 0x04000B89 RID: 2953
		private bool isOwnerFieldSpecified;

		// Token: 0x04000B8A RID: 2954
		private string contentOwnerField;

		// Token: 0x04000B8B RID: 2955
		private string contentExpiryDateField;
	}
}
