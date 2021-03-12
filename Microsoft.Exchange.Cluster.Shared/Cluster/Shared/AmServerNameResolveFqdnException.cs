using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000E5 RID: 229
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmServerNameResolveFqdnException : TransientException
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x0001CA07 File Offset: 0x0001AC07
		public AmServerNameResolveFqdnException(string error) : base(Strings.AmServerNameResolveFqdnException(error))
		{
			this.error = error;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001CA1C File Offset: 0x0001AC1C
		public AmServerNameResolveFqdnException(string error, Exception innerException) : base(Strings.AmServerNameResolveFqdnException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001CA32 File Offset: 0x0001AC32
		protected AmServerNameResolveFqdnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001CA77 File Offset: 0x0001AC77
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000740 RID: 1856
		private readonly string error;
	}
}
