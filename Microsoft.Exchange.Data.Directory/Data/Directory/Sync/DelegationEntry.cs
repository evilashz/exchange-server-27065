using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200084B RID: 2123
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DelegationEntry : DirectoryLinkServicePrincipalToServicePrincipal
	{
		// Token: 0x06006A23 RID: 27171 RVA: 0x001726D1 File Offset: 0x001708D1
		public override DirectoryObjectClass GetSourceClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.SourceClass.ToString());
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x001726F7 File Offset: 0x001708F7
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x001726FE File Offset: 0x001708FE
		public override DirectoryObjectClass GetTargetClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.TargetClass.ToString());
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x00172724 File Offset: 0x00170924
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170025B5 RID: 9653
		// (get) Token: 0x06006A27 RID: 27175 RVA: 0x0017272B File Offset: 0x0017092B
		// (set) Token: 0x06006A28 RID: 27176 RVA: 0x00172733 File Offset: 0x00170933
		[XmlElement(Order = 0)]
		public DirectoryPropertyInt32SingleMin0Max1 DelegationEncodingVersion
		{
			get
			{
				return this.delegationEncodingVersionField;
			}
			set
			{
				this.delegationEncodingVersionField = value;
			}
		}

		// Token: 0x170025B6 RID: 9654
		// (get) Token: 0x06006A29 RID: 27177 RVA: 0x0017273C File Offset: 0x0017093C
		// (set) Token: 0x06006A2A RID: 27178 RVA: 0x00172744 File Offset: 0x00170944
		[XmlElement(Order = 1)]
		public DirectoryPropertyInt32SingleMin0Max2 DelegationType
		{
			get
			{
				return this.delegationTypeField;
			}
			set
			{
				this.delegationTypeField = value;
			}
		}

		// Token: 0x170025B7 RID: 9655
		// (get) Token: 0x06006A2B RID: 27179 RVA: 0x0017274D File Offset: 0x0017094D
		// (set) Token: 0x06006A2C RID: 27180 RVA: 0x00172755 File Offset: 0x00170955
		[XmlElement(Order = 2)]
		public DirectoryPropertyInt32SingleMin0Max2 UserIdentifierType
		{
			get
			{
				return this.userIdentifierTypeField;
			}
			set
			{
				this.userIdentifierTypeField = value;
			}
		}

		// Token: 0x170025B8 RID: 9656
		// (get) Token: 0x06006A2D RID: 27181 RVA: 0x0017275E File Offset: 0x0017095E
		// (set) Token: 0x06006A2E RID: 27182 RVA: 0x00172766 File Offset: 0x00170966
		[XmlElement(Order = 3)]
		public DirectoryPropertyBinarySingleLength1To195 UserIdentifier
		{
			get
			{
				return this.userIdentifierField;
			}
			set
			{
				this.userIdentifierField = value;
			}
		}

		// Token: 0x170025B9 RID: 9657
		// (get) Token: 0x06006A2F RID: 27183 RVA: 0x0017276F File Offset: 0x0017096F
		// (set) Token: 0x06006A30 RID: 27184 RVA: 0x00172777 File Offset: 0x00170977
		[XmlElement(Order = 4)]
		public DirectoryPropertyDateTimeSingle DelegationEndTime
		{
			get
			{
				return this.delegationEndTimeField;
			}
			set
			{
				this.delegationEndTimeField = value;
			}
		}

		// Token: 0x170025BA RID: 9658
		// (get) Token: 0x06006A31 RID: 27185 RVA: 0x00172780 File Offset: 0x00170980
		// (set) Token: 0x06006A32 RID: 27186 RVA: 0x00172788 File Offset: 0x00170988
		[XmlElement(Order = 5)]
		public DirectoryPropertyBinarySingleLength1To8000 DelegationScope
		{
			get
			{
				return this.delegationScopeField;
			}
			set
			{
				this.delegationScopeField = value;
			}
		}

		// Token: 0x0400457A RID: 17786
		private DirectoryPropertyInt32SingleMin0Max1 delegationEncodingVersionField;

		// Token: 0x0400457B RID: 17787
		private DirectoryPropertyInt32SingleMin0Max2 delegationTypeField;

		// Token: 0x0400457C RID: 17788
		private DirectoryPropertyInt32SingleMin0Max2 userIdentifierTypeField;

		// Token: 0x0400457D RID: 17789
		private DirectoryPropertyBinarySingleLength1To195 userIdentifierField;

		// Token: 0x0400457E RID: 17790
		private DirectoryPropertyDateTimeSingle delegationEndTimeField;

		// Token: 0x0400457F RID: 17791
		private DirectoryPropertyBinarySingleLength1To8000 delegationScopeField;
	}
}
