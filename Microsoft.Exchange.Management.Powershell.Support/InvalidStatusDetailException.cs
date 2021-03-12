using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000055 RID: 85
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidStatusDetailException : LocalizedException
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x000113E1 File Offset: 0x0000F5E1
		public InvalidStatusDetailException(string uri) : base(Strings.InvalidStatusDetailError(uri))
		{
			this.uri = uri;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000113F6 File Offset: 0x0000F5F6
		public InvalidStatusDetailException(string uri, Exception innerException) : base(Strings.InvalidStatusDetailError(uri), innerException)
		{
			this.uri = uri;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001140C File Offset: 0x0000F60C
		protected InvalidStatusDetailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uri = (string)info.GetValue("uri", typeof(string));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00011436 File Offset: 0x0000F636
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uri", this.uri);
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00011451 File Offset: 0x0000F651
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x040001C5 RID: 453
		private readonly string uri;
	}
}
