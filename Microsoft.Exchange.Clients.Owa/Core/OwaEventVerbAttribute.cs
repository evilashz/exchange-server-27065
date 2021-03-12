using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000194 RID: 404
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class OwaEventVerbAttribute : Attribute
	{
		// Token: 0x06000EB9 RID: 3769 RVA: 0x0005D813 File Offset: 0x0005BA13
		public OwaEventVerbAttribute(OwaEventVerb verb)
		{
			this.verb = verb;
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0005D829 File Offset: 0x0005BA29
		public OwaEventVerb Verb
		{
			get
			{
				return this.verb;
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0005D831 File Offset: 0x0005BA31
		public static OwaEventVerb Parse(string verb)
		{
			if (verb == null)
			{
				throw new ArgumentNullException("verb");
			}
			if (string.Equals(verb, "POST", StringComparison.OrdinalIgnoreCase))
			{
				return OwaEventVerb.Post;
			}
			if (string.Equals(verb, "GET", StringComparison.OrdinalIgnoreCase))
			{
				return OwaEventVerb.Get;
			}
			return OwaEventVerb.Unsupported;
		}

		// Token: 0x04000A09 RID: 2569
		private OwaEventVerb verb = OwaEventVerb.Post;
	}
}
