using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003CB RID: 971
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckCorruptFileException : FileCheckException
	{
		// Token: 0x0600285C RID: 10332 RVA: 0x000B7BE1 File Offset: 0x000B5DE1
		public FileCheckCorruptFileException(string file, string errorMessage) : base(ReplayStrings.FileCheckCorruptFile(file, errorMessage))
		{
			this.file = file;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000B7C03 File Offset: 0x000B5E03
		public FileCheckCorruptFileException(string file, string errorMessage, Exception innerException) : base(ReplayStrings.FileCheckCorruptFile(file, errorMessage), innerException)
		{
			this.file = file;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000B7C28 File Offset: 0x000B5E28
		protected FileCheckCorruptFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000B7C7D File Offset: 0x000B5E7D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x000B7CA9 File Offset: 0x000B5EA9
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x000B7CB1 File Offset: 0x000B5EB1
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040013D7 RID: 5079
		private readonly string file;

		// Token: 0x040013D8 RID: 5080
		private readonly string errorMessage;
	}
}
