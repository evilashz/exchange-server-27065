using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001065 RID: 4197
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PAMCouldNotBeDeterminedException : LocalizedException
	{
		// Token: 0x0600B0CE RID: 45262 RVA: 0x00296D42 File Offset: 0x00294F42
		public PAMCouldNotBeDeterminedException(string dagName, string errorMsg) : base(Strings.PAMCouldNotBeDeterminedException(dagName, errorMsg))
		{
			this.dagName = dagName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600B0CF RID: 45263 RVA: 0x00296D5F File Offset: 0x00294F5F
		public PAMCouldNotBeDeterminedException(string dagName, string errorMsg, Exception innerException) : base(Strings.PAMCouldNotBeDeterminedException(dagName, errorMsg), innerException)
		{
			this.dagName = dagName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600B0D0 RID: 45264 RVA: 0x00296D80 File Offset: 0x00294F80
		protected PAMCouldNotBeDeterminedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600B0D1 RID: 45265 RVA: 0x00296DD5 File Offset: 0x00294FD5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17003857 RID: 14423
		// (get) Token: 0x0600B0D2 RID: 45266 RVA: 0x00296E01 File Offset: 0x00295001
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x17003858 RID: 14424
		// (get) Token: 0x0600B0D3 RID: 45267 RVA: 0x00296E09 File Offset: 0x00295009
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x040061BD RID: 25021
		private readonly string dagName;

		// Token: 0x040061BE RID: 25022
		private readonly string errorMsg;
	}
}
