using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000AD0 RID: 2768
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NonUniquePilotIdentifierException : LocalizedException
	{
		// Token: 0x060080D4 RID: 32980 RVA: 0x001A5F00 File Offset: 0x001A4100
		public NonUniquePilotIdentifierException(string pilotId, string dialPlan) : base(DirectoryStrings.NonUniquePilotIdentifier(pilotId, dialPlan))
		{
			this.pilotId = pilotId;
			this.dialPlan = dialPlan;
		}

		// Token: 0x060080D5 RID: 32981 RVA: 0x001A5F1D File Offset: 0x001A411D
		public NonUniquePilotIdentifierException(string pilotId, string dialPlan, Exception innerException) : base(DirectoryStrings.NonUniquePilotIdentifier(pilotId, dialPlan), innerException)
		{
			this.pilotId = pilotId;
			this.dialPlan = dialPlan;
		}

		// Token: 0x060080D6 RID: 32982 RVA: 0x001A5F3C File Offset: 0x001A413C
		protected NonUniquePilotIdentifierException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.pilotId = (string)info.GetValue("pilotId", typeof(string));
			this.dialPlan = (string)info.GetValue("dialPlan", typeof(string));
		}

		// Token: 0x060080D7 RID: 32983 RVA: 0x001A5F91 File Offset: 0x001A4191
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("pilotId", this.pilotId);
			info.AddValue("dialPlan", this.dialPlan);
		}

		// Token: 0x17002EEB RID: 12011
		// (get) Token: 0x060080D8 RID: 32984 RVA: 0x001A5FBD File Offset: 0x001A41BD
		public string PilotId
		{
			get
			{
				return this.pilotId;
			}
		}

		// Token: 0x17002EEC RID: 12012
		// (get) Token: 0x060080D9 RID: 32985 RVA: 0x001A5FC5 File Offset: 0x001A41C5
		public string DialPlan
		{
			get
			{
				return this.dialPlan;
			}
		}

		// Token: 0x040055C5 RID: 21957
		private readonly string pilotId;

		// Token: 0x040055C6 RID: 21958
		private readonly string dialPlan;
	}
}
