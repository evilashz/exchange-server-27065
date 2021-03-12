using System;
using System.Web;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000791 RID: 1937
	internal class UriHandler : UriBuilder, IComparable, IEquatable<UriHandler>
	{
		// Token: 0x06004442 RID: 17474 RVA: 0x001182F6 File Offset: 0x001164F6
		public UriHandler() : this(string.Empty)
		{
		}

		// Token: 0x06004443 RID: 17475 RVA: 0x00118303 File Offset: 0x00116503
		public UriHandler(string uri) : this(new Uri(uri))
		{
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x00118311 File Offset: 0x00116511
		public UriHandler(Uri uri) : base(uri)
		{
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x0011831A File Offset: 0x0011651A
		public UriHandler(Uri uri, string id) : base(uri)
		{
			this.Id = HttpUtility.UrlEncode(id);
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x0011832F File Offset: 0x0011652F
		public UriHandler(string user, string messageId)
		{
			base.Scheme = "excallog";
			base.Host = user;
			this.Id = messageId;
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06004447 RID: 17479 RVA: 0x00118350 File Offset: 0x00116550
		public bool IsValidLink
		{
			get
			{
				return base.Scheme == "excallog" || base.Scheme == "file";
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x00118376 File Offset: 0x00116576
		public bool IsMailboxLink
		{
			get
			{
				return base.Scheme == "excallog";
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x00118388 File Offset: 0x00116588
		public bool IsFileLink
		{
			get
			{
				return base.Scheme == "file";
			}
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x0011839A File Offset: 0x0011659A
		// (set) Token: 0x0600444B RID: 17483 RVA: 0x001183D0 File Offset: 0x001165D0
		public string Id
		{
			get
			{
				if (!string.IsNullOrEmpty(base.Query))
				{
					return base.Query.Substring(string.Format("{0}id=", '?').Length);
				}
				return string.Empty;
			}
			set
			{
				base.Query = string.Format("id={0}", value);
			}
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x001183E3 File Offset: 0x001165E3
		public static implicit operator Uri(UriHandler uh)
		{
			if (uh == null)
			{
				return null;
			}
			return uh.Uri;
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x001183F0 File Offset: 0x001165F0
		protected virtual bool CheckEquality(UriHandler other)
		{
			return other != null && !(this.Id != other.Id) && !(base.Scheme != other.Scheme) && !(base.Host != other.Host) && !(base.UserName != other.UserName) && !(base.Path != other.Path);
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x0011846C File Offset: 0x0011666C
		public bool Equals(UriHandler other)
		{
			return this.CheckEquality(other);
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x00118475 File Offset: 0x00116675
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", string.Empty);
			}
			return string.Compare(base.Uri.AbsoluteUri, ((UriHandler)obj).Uri.AbsoluteUri, StringComparison.Ordinal);
		}

		// Token: 0x04002A63 RID: 10851
		private const int ItemIdSegment = 1;

		// Token: 0x04002A64 RID: 10852
		public const char QueryDesignator = '?';

		// Token: 0x02000792 RID: 1938
		internal abstract class Schemes
		{
			// Token: 0x04002A65 RID: 10853
			public const string Mailbox = "excallog";

			// Token: 0x04002A66 RID: 10854
			public const string File = "file";
		}
	}
}
