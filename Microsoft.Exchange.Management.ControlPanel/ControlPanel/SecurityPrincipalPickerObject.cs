using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000354 RID: 852
	[DataContract]
	[KnownType(typeof(SecurityPrincipalPickerObject))]
	public class SecurityPrincipalPickerObject : BaseRow
	{
		// Token: 0x06002F9C RID: 12188 RVA: 0x0009140D File Offset: 0x0008F60D
		public SecurityPrincipalPickerObject(ExtendedSecurityPrincipal securityPrincipal) : base(securityPrincipal)
		{
			this.ExtendedSecurityPrincipal = securityPrincipal;
		}

		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x0009141D File Offset: 0x0008F61D
		// (set) Token: 0x06002F9E RID: 12190 RVA: 0x00091425 File Offset: 0x0008F625
		private ExtendedSecurityPrincipal ExtendedSecurityPrincipal { get; set; }

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x0009142E File Offset: 0x0008F62E
		// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x0009143B File Offset: 0x0008F63B
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.ExtendedSecurityPrincipal.DisplayName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F08 RID: 7944
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x00091442 File Offset: 0x0008F642
		// (set) Token: 0x06002FA2 RID: 12194 RVA: 0x0009144F File Offset: 0x0008F64F
		[DataMember]
		public string InFolder
		{
			get
			{
				return this.ExtendedSecurityPrincipal.InFolder;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x00091456 File Offset: 0x0008F656
		// (set) Token: 0x06002FA4 RID: 12196 RVA: 0x00091463 File Offset: 0x0008F663
		[DataMember]
		public string Name
		{
			get
			{
				return this.ExtendedSecurityPrincipal.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x0009146A File Offset: 0x0008F66A
		// (set) Token: 0x06002FA6 RID: 12198 RVA: 0x0009147C File Offset: 0x0008F67C
		[DataMember]
		public string SpriteId
		{
			get
			{
				return Icons.FromEnum(this.ExtendedSecurityPrincipal.Type);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x00091483 File Offset: 0x0008F683
		// (set) Token: 0x06002FA8 RID: 12200 RVA: 0x00091495 File Offset: 0x0008F695
		[DataMember]
		public string IconAltText
		{
			get
			{
				return Icons.GenerateIconAltText(this.ExtendedSecurityPrincipal.Type);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
