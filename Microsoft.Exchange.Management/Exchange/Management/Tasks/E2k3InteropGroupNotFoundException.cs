using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E06 RID: 3590
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class E2k3InteropGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A519 RID: 42265 RVA: 0x00285689 File Offset: 0x00283889
		public E2k3InteropGroupNotFoundException(Guid guid) : base(Strings.E2k3InteropGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x0600A51A RID: 42266 RVA: 0x0028569E File Offset: 0x0028389E
		public E2k3InteropGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.E2k3InteropGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x0600A51B RID: 42267 RVA: 0x002856B4 File Offset: 0x002838B4
		protected E2k3InteropGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x0600A51C RID: 42268 RVA: 0x002856DE File Offset: 0x002838DE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x1700361E RID: 13854
		// (get) Token: 0x0600A51D RID: 42269 RVA: 0x002856FE File Offset: 0x002838FE
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04005F84 RID: 24452
		private readonly Guid guid;
	}
}
