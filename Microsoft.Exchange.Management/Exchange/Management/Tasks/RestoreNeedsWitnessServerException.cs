using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001059 RID: 4185
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RestoreNeedsWitnessServerException : LocalizedException
	{
		// Token: 0x0600B08D RID: 45197 RVA: 0x002965F5 File Offset: 0x002947F5
		public RestoreNeedsWitnessServerException(string dagName) : base(Strings.RestoreNeedsWitnessServer(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B08E RID: 45198 RVA: 0x0029660A File Offset: 0x0029480A
		public RestoreNeedsWitnessServerException(string dagName, Exception innerException) : base(Strings.RestoreNeedsWitnessServer(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B08F RID: 45199 RVA: 0x00296620 File Offset: 0x00294820
		protected RestoreNeedsWitnessServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B090 RID: 45200 RVA: 0x0029664A File Offset: 0x0029484A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003846 RID: 14406
		// (get) Token: 0x0600B091 RID: 45201 RVA: 0x00296665 File Offset: 0x00294865
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040061AC RID: 25004
		private readonly string dagName;
	}
}
