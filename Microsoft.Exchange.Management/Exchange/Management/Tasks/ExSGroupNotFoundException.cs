using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E02 RID: 3586
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExSGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A503 RID: 42243 RVA: 0x002853F5 File Offset: 0x002835F5
		public ExSGroupNotFoundException(Guid guid) : base(Strings.ExSGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x0600A504 RID: 42244 RVA: 0x0028540A File Offset: 0x0028360A
		public ExSGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.ExSGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x0600A505 RID: 42245 RVA: 0x00285420 File Offset: 0x00283620
		protected ExSGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x0600A506 RID: 42246 RVA: 0x0028544A File Offset: 0x0028364A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x17003618 RID: 13848
		// (get) Token: 0x0600A507 RID: 42247 RVA: 0x0028546A File Offset: 0x0028366A
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04005F7E RID: 24446
		private readonly Guid guid;
	}
}
