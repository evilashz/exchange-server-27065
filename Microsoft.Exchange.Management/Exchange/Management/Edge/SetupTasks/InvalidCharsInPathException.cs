using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001228 RID: 4648
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCharsInPathException : LocalizedException
	{
		// Token: 0x0600BB28 RID: 47912 RVA: 0x002A9DA6 File Offset: 0x002A7FA6
		public InvalidCharsInPathException(string path) : base(Strings.InvalidCharsInPath(path))
		{
			this.path = path;
		}

		// Token: 0x0600BB29 RID: 47913 RVA: 0x002A9DBB File Offset: 0x002A7FBB
		public InvalidCharsInPathException(string path, Exception innerException) : base(Strings.InvalidCharsInPath(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x0600BB2A RID: 47914 RVA: 0x002A9DD1 File Offset: 0x002A7FD1
		protected InvalidCharsInPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x0600BB2B RID: 47915 RVA: 0x002A9DFB File Offset: 0x002A7FFB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x17003AE8 RID: 15080
		// (get) Token: 0x0600BB2C RID: 47916 RVA: 0x002A9E16 File Offset: 0x002A8016
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04006570 RID: 25968
		private readonly string path;
	}
}
