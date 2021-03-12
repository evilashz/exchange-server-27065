using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E03 RID: 3587
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MaSGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A508 RID: 42248 RVA: 0x00285472 File Offset: 0x00283672
		public MaSGroupNotFoundException(Guid guid) : base(Strings.MaSGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x0600A509 RID: 42249 RVA: 0x00285487 File Offset: 0x00283687
		public MaSGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.MaSGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x0600A50A RID: 42250 RVA: 0x0028549D File Offset: 0x0028369D
		protected MaSGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x0600A50B RID: 42251 RVA: 0x002854C7 File Offset: 0x002836C7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x17003619 RID: 13849
		// (get) Token: 0x0600A50C RID: 42252 RVA: 0x002854E7 File Offset: 0x002836E7
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04005F7F RID: 24447
		private readonly Guid guid;
	}
}
