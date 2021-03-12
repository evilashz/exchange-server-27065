using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001229 RID: 4649
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PathIsTooLongException : LocalizedException
	{
		// Token: 0x0600BB2D RID: 47917 RVA: 0x002A9E1E File Offset: 0x002A801E
		public PathIsTooLongException(string path) : base(Strings.PathTooLong(path))
		{
			this.path = path;
		}

		// Token: 0x0600BB2E RID: 47918 RVA: 0x002A9E33 File Offset: 0x002A8033
		public PathIsTooLongException(string path, Exception innerException) : base(Strings.PathTooLong(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x0600BB2F RID: 47919 RVA: 0x002A9E49 File Offset: 0x002A8049
		protected PathIsTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x0600BB30 RID: 47920 RVA: 0x002A9E73 File Offset: 0x002A8073
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x17003AE9 RID: 15081
		// (get) Token: 0x0600BB31 RID: 47921 RVA: 0x002A9E8E File Offset: 0x002A808E
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04006571 RID: 25969
		private readonly string path;
	}
}
