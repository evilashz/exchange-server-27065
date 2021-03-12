using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FB0 RID: 4016
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoveVirtualDirectoryGetApplicationPoolUnknownError : LocalizedException
	{
		// Token: 0x0600AD40 RID: 44352 RVA: 0x00291505 File Offset: 0x0028F705
		public RemoveVirtualDirectoryGetApplicationPoolUnknownError(string path) : base(Strings.RemoveVirtualDirectoryGetApplicationPoolUnknownError(path))
		{
			this.path = path;
		}

		// Token: 0x0600AD41 RID: 44353 RVA: 0x0029151A File Offset: 0x0028F71A
		public RemoveVirtualDirectoryGetApplicationPoolUnknownError(string path, Exception innerException) : base(Strings.RemoveVirtualDirectoryGetApplicationPoolUnknownError(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x0600AD42 RID: 44354 RVA: 0x00291530 File Offset: 0x0028F730
		protected RemoveVirtualDirectoryGetApplicationPoolUnknownError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x0600AD43 RID: 44355 RVA: 0x0029155A File Offset: 0x0028F75A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x1700379D RID: 14237
		// (get) Token: 0x0600AD44 RID: 44356 RVA: 0x00291575 File Offset: 0x0028F775
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04006103 RID: 24835
		private readonly string path;
	}
}
