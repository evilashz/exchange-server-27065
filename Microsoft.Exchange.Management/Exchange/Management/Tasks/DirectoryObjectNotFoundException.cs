using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E1C RID: 3612
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DirectoryObjectNotFoundException : LocalizedException
	{
		// Token: 0x0600A58E RID: 42382 RVA: 0x00286361 File Offset: 0x00284561
		public DirectoryObjectNotFoundException(string dn) : base(Strings.DirectoryObjectNotFoundException(dn))
		{
			this.dn = dn;
		}

		// Token: 0x0600A58F RID: 42383 RVA: 0x00286376 File Offset: 0x00284576
		public DirectoryObjectNotFoundException(string dn, Exception innerException) : base(Strings.DirectoryObjectNotFoundException(dn), innerException)
		{
			this.dn = dn;
		}

		// Token: 0x0600A590 RID: 42384 RVA: 0x0028638C File Offset: 0x0028458C
		protected DirectoryObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dn = (string)info.GetValue("dn", typeof(string));
		}

		// Token: 0x0600A591 RID: 42385 RVA: 0x002863B6 File Offset: 0x002845B6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dn", this.dn);
		}

		// Token: 0x1700363B RID: 13883
		// (get) Token: 0x0600A592 RID: 42386 RVA: 0x002863D1 File Offset: 0x002845D1
		public string Dn
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x04005FA1 RID: 24481
		private readonly string dn;
	}
}
