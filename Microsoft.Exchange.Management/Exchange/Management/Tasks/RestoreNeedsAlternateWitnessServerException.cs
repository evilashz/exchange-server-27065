using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001058 RID: 4184
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RestoreNeedsAlternateWitnessServerException : LocalizedException
	{
		// Token: 0x0600B088 RID: 45192 RVA: 0x0029657D File Offset: 0x0029477D
		public RestoreNeedsAlternateWitnessServerException(string dagName) : base(Strings.RestoreNeedsAlternateWitnessServer(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B089 RID: 45193 RVA: 0x00296592 File Offset: 0x00294792
		public RestoreNeedsAlternateWitnessServerException(string dagName, Exception innerException) : base(Strings.RestoreNeedsAlternateWitnessServer(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B08A RID: 45194 RVA: 0x002965A8 File Offset: 0x002947A8
		protected RestoreNeedsAlternateWitnessServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B08B RID: 45195 RVA: 0x002965D2 File Offset: 0x002947D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003845 RID: 14405
		// (get) Token: 0x0600B08C RID: 45196 RVA: 0x002965ED File Offset: 0x002947ED
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040061AB RID: 25003
		private readonly string dagName;
	}
}
