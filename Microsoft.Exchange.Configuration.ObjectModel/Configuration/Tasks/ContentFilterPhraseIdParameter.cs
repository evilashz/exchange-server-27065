using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public class ContentFilterPhraseIdParameter : IIdentityParameter
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x0001FA85 File Offset: 0x0001DC85
		public ContentFilterPhraseIdParameter(string phrase)
		{
			this.phrase = phrase;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001FA94 File Offset: 0x0001DC94
		public ContentFilterPhraseIdParameter(ContentFilterPhrase phrase)
		{
			if (phrase == null)
			{
				throw new ArgumentException(Strings.ExArgumentNullException("phrase"), "phrase");
			}
			this.phrase = phrase.Phrase;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001FAC5 File Offset: 0x0001DCC5
		public ContentFilterPhraseIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001FAD3 File Offset: 0x0001DCD3
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0001FADB File Offset: 0x0001DCDB
		internal string RawIdentity
		{
			get
			{
				return this.phrase;
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001FAE3 File Offset: 0x0001DCE3
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.GetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001FAF0 File Offset: 0x0001DCF0
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001FB08 File Offset: 0x0001DD08
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001FB14 File Offset: 0x0001DD14
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			if (!(session is ContentFilterPhraseDataProvider))
			{
				throw new ArgumentException(Strings.ErrorInvalidType((session != null) ? session.GetType().Name : "null"), "session");
			}
			notFoundReason = null;
			IConfigurable configurable = session.Read<T>(new ContentFilterPhraseIdentity(this.phrase));
			T[] result;
			if (configurable != null)
			{
				result = new T[]
				{
					(T)((object)configurable)
				};
			}
			else
			{
				result = new T[0];
			}
			return result;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001FB90 File Offset: 0x0001DD90
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		internal void Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity("null"), "objectId");
			}
			ContentFilterPhraseIdentity contentFilterPhraseIdentity = objectId as ContentFilterPhraseIdentity;
			if (contentFilterPhraseIdentity == null)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterType("objectId", typeof(ContentFilterPhraseIdentity).Name), "objectId");
			}
			this.phrase = Encoding.Unicode.GetString(contentFilterPhraseIdentity.GetBytes());
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001FC0F File Offset: 0x0001DE0F
		public override string ToString()
		{
			return this.phrase;
		}

		// Token: 0x04000265 RID: 613
		private string phrase;
	}
}
