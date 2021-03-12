using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001048 RID: 4168
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagCantBeSetIntoDatacenterActivationModeException : LocalizedException
	{
		// Token: 0x0600B025 RID: 45093 RVA: 0x002957EC File Offset: 0x002939EC
		public DagCantBeSetIntoDatacenterActivationModeException(string dagName) : base(Strings.DagCantBeSetIntoDatacenterActivationMode(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B026 RID: 45094 RVA: 0x00295801 File Offset: 0x00293A01
		public DagCantBeSetIntoDatacenterActivationModeException(string dagName, Exception innerException) : base(Strings.DagCantBeSetIntoDatacenterActivationMode(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B027 RID: 45095 RVA: 0x00295817 File Offset: 0x00293A17
		protected DagCantBeSetIntoDatacenterActivationModeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B028 RID: 45096 RVA: 0x00295841 File Offset: 0x00293A41
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003822 RID: 14370
		// (get) Token: 0x0600B029 RID: 45097 RVA: 0x0029585C File Offset: 0x00293A5C
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x04006188 RID: 24968
		private readonly string dagName;
	}
}
