using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x020001C7 RID: 455
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceFileNotFoundException : PublishingException
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x00035E68 File Offset: 0x00034068
		public SourceFileNotFoundException(string path) : base(Strings.SourceFileNotFound(path))
		{
			this.path = path;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00035E7D File Offset: 0x0003407D
		public SourceFileNotFoundException(string path, Exception innerException) : base(Strings.SourceFileNotFound(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00035E93 File Offset: 0x00034093
		protected SourceFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00035EBD File Offset: 0x000340BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00035ED8 File Offset: 0x000340D8
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x0400079D RID: 1949
		private readonly string path;
	}
}
