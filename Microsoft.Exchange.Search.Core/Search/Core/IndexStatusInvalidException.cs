using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000C5 RID: 197
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IndexStatusInvalidException : IndexStatusException
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x00012FE3 File Offset: 0x000111E3
		public IndexStatusInvalidException(string value) : base(Strings.IndexStatusInvalid(value))
		{
			this.value = value;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00012FFD File Offset: 0x000111FD
		public IndexStatusInvalidException(string value, Exception innerException) : base(Strings.IndexStatusInvalid(value), innerException)
		{
			this.value = value;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00013018 File Offset: 0x00011218
		protected IndexStatusInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00013042 File Offset: 0x00011242
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("value", this.value);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001305D File Offset: 0x0001125D
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002CC RID: 716
		private readonly string value;
	}
}
