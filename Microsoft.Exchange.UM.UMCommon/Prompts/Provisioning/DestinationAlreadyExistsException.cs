using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x020001C8 RID: 456
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DestinationAlreadyExistsException : PublishingException
	{
		// Token: 0x06000F0E RID: 3854 RVA: 0x00035EE0 File Offset: 0x000340E0
		public DestinationAlreadyExistsException(string path) : base(Strings.DestinationAlreadyExists(path))
		{
			this.path = path;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00035EF5 File Offset: 0x000340F5
		public DestinationAlreadyExistsException(string path, Exception innerException) : base(Strings.DestinationAlreadyExists(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00035F0B File Offset: 0x0003410B
		protected DestinationAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00035F35 File Offset: 0x00034135
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00035F50 File Offset: 0x00034150
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x0400079E RID: 1950
		private readonly string path;
	}
}
