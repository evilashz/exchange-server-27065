using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001055 RID: 4181
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswAndAlternateFswOnSameWitnessServerButPointToDifferentDirectoriesException : LocalizedException
	{
		// Token: 0x0600B074 RID: 45172 RVA: 0x0029627A File Offset: 0x0029447A
		public DagFswAndAlternateFswOnSameWitnessServerButPointToDifferentDirectoriesException(string witnessserver, string witnessdirectory, string alternatewitnessdirectory) : base(Strings.DagFswAndAlternateFswOnSameWitnessServerButPointToDifferentDirectoriesException(witnessserver, witnessdirectory, alternatewitnessdirectory))
		{
			this.witnessserver = witnessserver;
			this.witnessdirectory = witnessdirectory;
			this.alternatewitnessdirectory = alternatewitnessdirectory;
		}

		// Token: 0x0600B075 RID: 45173 RVA: 0x0029629F File Offset: 0x0029449F
		public DagFswAndAlternateFswOnSameWitnessServerButPointToDifferentDirectoriesException(string witnessserver, string witnessdirectory, string alternatewitnessdirectory, Exception innerException) : base(Strings.DagFswAndAlternateFswOnSameWitnessServerButPointToDifferentDirectoriesException(witnessserver, witnessdirectory, alternatewitnessdirectory), innerException)
		{
			this.witnessserver = witnessserver;
			this.witnessdirectory = witnessdirectory;
			this.alternatewitnessdirectory = alternatewitnessdirectory;
		}

		// Token: 0x0600B076 RID: 45174 RVA: 0x002962C8 File Offset: 0x002944C8
		protected DagFswAndAlternateFswOnSameWitnessServerButPointToDifferentDirectoriesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.witnessserver = (string)info.GetValue("witnessserver", typeof(string));
			this.witnessdirectory = (string)info.GetValue("witnessdirectory", typeof(string));
			this.alternatewitnessdirectory = (string)info.GetValue("alternatewitnessdirectory", typeof(string));
		}

		// Token: 0x0600B077 RID: 45175 RVA: 0x0029633D File Offset: 0x0029453D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("witnessserver", this.witnessserver);
			info.AddValue("witnessdirectory", this.witnessdirectory);
			info.AddValue("alternatewitnessdirectory", this.alternatewitnessdirectory);
		}

		// Token: 0x1700383D RID: 14397
		// (get) Token: 0x0600B078 RID: 45176 RVA: 0x0029637A File Offset: 0x0029457A
		public string Witnessserver
		{
			get
			{
				return this.witnessserver;
			}
		}

		// Token: 0x1700383E RID: 14398
		// (get) Token: 0x0600B079 RID: 45177 RVA: 0x00296382 File Offset: 0x00294582
		public string Witnessdirectory
		{
			get
			{
				return this.witnessdirectory;
			}
		}

		// Token: 0x1700383F RID: 14399
		// (get) Token: 0x0600B07A RID: 45178 RVA: 0x0029638A File Offset: 0x0029458A
		public string Alternatewitnessdirectory
		{
			get
			{
				return this.alternatewitnessdirectory;
			}
		}

		// Token: 0x040061A3 RID: 24995
		private readonly string witnessserver;

		// Token: 0x040061A4 RID: 24996
		private readonly string witnessdirectory;

		// Token: 0x040061A5 RID: 24997
		private readonly string alternatewitnessdirectory;
	}
}
