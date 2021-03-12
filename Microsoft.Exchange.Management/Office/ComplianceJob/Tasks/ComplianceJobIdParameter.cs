using System;
using System.Collections.Generic;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	public class ComplianceJobIdParameter : IIdentityParameter
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x00035736 File Offset: 0x00033936
		public ComplianceJobIdParameter()
		{
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0003573E File Offset: 0x0003393E
		public ComplianceJobIdParameter(ComplianceJob job) : this(((ComplianceJobId)job.Identity).Guid)
		{
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00035756 File Offset: 0x00033956
		public ComplianceJobIdParameter(Guid identity)
		{
			this.jobId = identity;
			this.rawIdentity = identity.ToString();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00035778 File Offset: 0x00033978
		public ComplianceJobIdParameter(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("ComplianceJobIdParameter(string identity)");
			}
			this.rawIdentity = identity;
			Guid guid;
			if (Guid.TryParse(identity, out guid))
			{
				this.jobId = guid;
				return;
			}
			this.displayName = identity;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000357BE File Offset: 0x000339BE
		public ComplianceJobIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.displayName = namedIdentity.DisplayName;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000357D8 File Offset: 0x000339D8
		public static ComplianceJobIdParameter Parse(string identity)
		{
			return new ComplianceJobIdParameter(identity);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000357E0 File Offset: 0x000339E0
		public override string ToString()
		{
			return this.displayName ?? this.rawIdentity;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000357F4 File Offset: 0x000339F4
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0003580C File Offset: 0x00033A0C
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (!(session is ComplianceJobProvider))
			{
				throw new NotSupportedException("session: " + session.GetType().FullName);
			}
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				throw new NotSupportedException("Supplying Additional Filters is not currently supported by this IdParameter.");
			}
			T t;
			if (string.IsNullOrEmpty(this.displayName))
			{
				t = (T)((object)((ComplianceJobProvider)session).Read<ComplianceSearch>(new ComplianceJobId(this.jobId)));
			}
			else
			{
				t = (T)((object)((ComplianceJobProvider)session).FindJobsByName<T>(this.displayName));
			}
			if (t == null)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.rawIdentity));
				return new T[0];
			}
			notFoundReason = null;
			return new T[]
			{
				t
			};
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000358E0 File Offset: 0x00033AE0
		public void Initialize(ObjectId objectId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x000358E7 File Offset: 0x00033AE7
		public string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x0400059C RID: 1436
		private readonly string displayName;

		// Token: 0x0400059D RID: 1437
		private readonly string rawIdentity;

		// Token: 0x0400059E RID: 1438
		[NonSerialized]
		private readonly Guid jobId;
	}
}
