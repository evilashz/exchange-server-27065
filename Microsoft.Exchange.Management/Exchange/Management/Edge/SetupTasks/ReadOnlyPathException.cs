using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200122A RID: 4650
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReadOnlyPathException : LocalizedException
	{
		// Token: 0x0600BB32 RID: 47922 RVA: 0x002A9E96 File Offset: 0x002A8096
		public ReadOnlyPathException(string path) : base(Strings.ReadOnlyPath(path))
		{
			this.path = path;
		}

		// Token: 0x0600BB33 RID: 47923 RVA: 0x002A9EAB File Offset: 0x002A80AB
		public ReadOnlyPathException(string path, Exception innerException) : base(Strings.ReadOnlyPath(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x0600BB34 RID: 47924 RVA: 0x002A9EC1 File Offset: 0x002A80C1
		protected ReadOnlyPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x0600BB35 RID: 47925 RVA: 0x002A9EEB File Offset: 0x002A80EB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x17003AEA RID: 15082
		// (get) Token: 0x0600BB36 RID: 47926 RVA: 0x002A9F06 File Offset: 0x002A8106
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04006572 RID: 25970
		private readonly string path;
	}
}
