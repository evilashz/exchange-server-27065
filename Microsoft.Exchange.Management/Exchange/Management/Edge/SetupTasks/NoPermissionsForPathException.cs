using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001227 RID: 4647
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoPermissionsForPathException : LocalizedException
	{
		// Token: 0x0600BB23 RID: 47907 RVA: 0x002A9D2E File Offset: 0x002A7F2E
		public NoPermissionsForPathException(string path) : base(Strings.NoPermissionsForPath(path))
		{
			this.path = path;
		}

		// Token: 0x0600BB24 RID: 47908 RVA: 0x002A9D43 File Offset: 0x002A7F43
		public NoPermissionsForPathException(string path, Exception innerException) : base(Strings.NoPermissionsForPath(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x0600BB25 RID: 47909 RVA: 0x002A9D59 File Offset: 0x002A7F59
		protected NoPermissionsForPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x0600BB26 RID: 47910 RVA: 0x002A9D83 File Offset: 0x002A7F83
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x17003AE7 RID: 15079
		// (get) Token: 0x0600BB27 RID: 47911 RVA: 0x002A9D9E File Offset: 0x002A7F9E
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x0400656F RID: 25967
		private readonly string path;
	}
}
