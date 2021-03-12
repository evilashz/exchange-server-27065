using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000DD RID: 221
	internal class PersistentConditionalRegistration : BaseConditionalRegistration
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x00024FD8 File Offset: 0x000231D8
		public static PersistentConditionalRegistration CreateFromXml(XElement element)
		{
			if (BaseConditionalRegistration.FetchSchema == null || BaseConditionalRegistration.QuerySchema == null || string.IsNullOrEmpty(ConditionalRegistrationLog.ProtocolName))
			{
				throw new InvalidOperationException("Can not use Conditional Diagnostics Handlers without proper initialization. Call 'BaseConditionalRegistration.Initialize' to initialize pre-requisites.");
			}
			XAttribute xattribute = element.Attribute(XName.Get("Name"));
			XAttribute xattribute2 = element.Attribute(XName.Get("Registration"));
			if (xattribute == null || xattribute2 == null)
			{
				throw new ArgumentException("[PersistentConditionalRegistration.CreateFromXml] app.config persistent registrations must have both 'Name' and 'Registration' attributes.");
			}
			string propertiesToFetch;
			string whereClause;
			BaseConditionalRegistration.ParseArgument(xattribute2.Value, out propertiesToFetch, out whereClause);
			return new PersistentConditionalRegistration(xattribute.Value, propertiesToFetch, whereClause);
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00025059 File Offset: 0x00023259
		public override bool ShouldEvaluate
		{
			get
			{
				return TimeProvider.UtcNow - base.LastHitUtc >= PersistentConditionalRegistration.MinimumTimeBetweenConditionalHitsEntry.Value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0002507A File Offset: 0x0002327A
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00025082 File Offset: 0x00023282
		public override string Description
		{
			get
			{
				return base.Cookie;
			}
			protected set
			{
				throw new NotSupportedException("Can't set the description on a persistent registration since it is the same as the cookie.");
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002508E File Offset: 0x0002328E
		public PersistentConditionalRegistration(string cookie, string propertiesToFetch, string whereClause) : base(cookie, "PersistentRegistration", propertiesToFetch, whereClause)
		{
		}

		// Token: 0x04000468 RID: 1128
		private const string PersistentRegistrationUser = "PersistentRegistration";

		// Token: 0x04000469 RID: 1129
		internal static TimeSpanAppSettingsEntry MinimumTimeBetweenConditionalHitsEntry = new TimeSpanAppSettingsEntry("MinimumTimeBetweenConditionalHits", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(1.0), null);
	}
}
