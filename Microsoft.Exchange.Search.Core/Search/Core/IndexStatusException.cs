using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000C2 RID: 194
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IndexStatusException : LocalizedException
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00012EB0 File Offset: 0x000110B0
		public IndexStatusException(string error) : base(Strings.IndexStatusException(error))
		{
			this.error = error;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00012EC5 File Offset: 0x000110C5
		public IndexStatusException(string error, Exception innerException) : base(Strings.IndexStatusException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00012EDB File Offset: 0x000110DB
		protected IndexStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00012F05 File Offset: 0x00011105
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00012F20 File Offset: 0x00011120
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040002CA RID: 714
		private readonly string error;
	}
}
