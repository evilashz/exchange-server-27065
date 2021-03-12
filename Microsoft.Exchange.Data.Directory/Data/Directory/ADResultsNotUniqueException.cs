using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ACB RID: 2763
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADResultsNotUniqueException : ADOperationException
	{
		// Token: 0x060080B9 RID: 32953 RVA: 0x001A5BFD File Offset: 0x001A3DFD
		public ADResultsNotUniqueException(string filter) : base(DirectoryStrings.ErrorResultsAreNonUnique(filter))
		{
			this.filter = filter;
		}

		// Token: 0x060080BA RID: 32954 RVA: 0x001A5C12 File Offset: 0x001A3E12
		public ADResultsNotUniqueException(string filter, Exception innerException) : base(DirectoryStrings.ErrorResultsAreNonUnique(filter), innerException)
		{
			this.filter = filter;
		}

		// Token: 0x060080BB RID: 32955 RVA: 0x001A5C28 File Offset: 0x001A3E28
		protected ADResultsNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filter = (string)info.GetValue("filter", typeof(string));
		}

		// Token: 0x060080BC RID: 32956 RVA: 0x001A5C52 File Offset: 0x001A3E52
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filter", this.filter);
		}

		// Token: 0x17002EE4 RID: 12004
		// (get) Token: 0x060080BD RID: 32957 RVA: 0x001A5C6D File Offset: 0x001A3E6D
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x040055BE RID: 21950
		private readonly string filter;
	}
}
