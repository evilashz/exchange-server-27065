using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000E5 RID: 229
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmFailedToDeterminePAM : AmServerTransientException
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x00068955 File Offset: 0x00066B55
		public AmFailedToDeterminePAM(string dagName) : base(ServerStrings.AmFailedToDeterminePAM(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0006896F File Offset: 0x00066B6F
		public AmFailedToDeterminePAM(string dagName, Exception innerException) : base(ServerStrings.AmFailedToDeterminePAM(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0006898A File Offset: 0x00066B8A
		protected AmFailedToDeterminePAM(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000689B4 File Offset: 0x00066BB4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x000689CF File Offset: 0x00066BCF
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x04000976 RID: 2422
		private readonly string dagName;
	}
}
