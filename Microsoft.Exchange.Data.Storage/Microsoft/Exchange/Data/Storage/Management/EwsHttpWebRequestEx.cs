using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009C5 RID: 2501
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EwsHttpWebRequestEx : IEwsHttpWebRequest
	{
		// Token: 0x06005C48 RID: 23624 RVA: 0x001812EA File Offset: 0x0017F4EA
		internal EwsHttpWebRequestEx(Uri uri)
		{
			this.request = (HttpWebRequest)WebRequest.Create(uri);
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x00181303 File Offset: 0x0017F503
		void IEwsHttpWebRequest.Abort()
		{
			this.request.Abort();
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x00181310 File Offset: 0x0017F510
		IAsyncResult IEwsHttpWebRequest.BeginGetRequestStream(AsyncCallback callback, object state)
		{
			return this.request.BeginGetRequestStream(callback, state);
		}

		// Token: 0x06005C4B RID: 23627 RVA: 0x0018131F File Offset: 0x0017F51F
		IAsyncResult IEwsHttpWebRequest.BeginGetResponse(AsyncCallback callback, object state)
		{
			return this.request.BeginGetResponse(callback, state);
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x0018132E File Offset: 0x0017F52E
		Stream IEwsHttpWebRequest.EndGetRequestStream(IAsyncResult asyncResult)
		{
			return this.request.EndGetRequestStream(asyncResult);
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x0018133C File Offset: 0x0017F53C
		IEwsHttpWebResponse IEwsHttpWebRequest.EndGetResponse(IAsyncResult asyncResult)
		{
			return new EwsHttpWebResponse((HttpWebResponse)this.request.EndGetResponse(asyncResult));
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x00181354 File Offset: 0x0017F554
		Stream IEwsHttpWebRequest.GetRequestStream()
		{
			return this.request.GetRequestStream();
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x00181361 File Offset: 0x0017F561
		IEwsHttpWebResponse IEwsHttpWebRequest.GetResponse()
		{
			return new EwsHttpWebResponse(this.request.GetResponse() as HttpWebResponse);
		}

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x06005C50 RID: 23632 RVA: 0x00181378 File Offset: 0x0017F578
		// (set) Token: 0x06005C51 RID: 23633 RVA: 0x00181385 File Offset: 0x0017F585
		string IEwsHttpWebRequest.Accept
		{
			get
			{
				return this.request.Accept;
			}
			set
			{
				this.request.Accept = value;
			}
		}

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x06005C52 RID: 23634 RVA: 0x00181393 File Offset: 0x0017F593
		// (set) Token: 0x06005C53 RID: 23635 RVA: 0x001813A0 File Offset: 0x0017F5A0
		bool IEwsHttpWebRequest.AllowAutoRedirect
		{
			get
			{
				return this.request.AllowAutoRedirect;
			}
			set
			{
				this.request.AllowAutoRedirect = value;
			}
		}

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x06005C54 RID: 23636 RVA: 0x001813AE File Offset: 0x0017F5AE
		// (set) Token: 0x06005C55 RID: 23637 RVA: 0x001813BB File Offset: 0x0017F5BB
		X509CertificateCollection IEwsHttpWebRequest.ClientCertificates
		{
			get
			{
				return this.request.ClientCertificates;
			}
			set
			{
				this.request.ClientCertificates = value;
			}
		}

		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x06005C56 RID: 23638 RVA: 0x001813C9 File Offset: 0x0017F5C9
		// (set) Token: 0x06005C57 RID: 23639 RVA: 0x001813D6 File Offset: 0x0017F5D6
		string IEwsHttpWebRequest.ContentType
		{
			get
			{
				return this.request.ContentType;
			}
			set
			{
				this.request.ContentType = value;
			}
		}

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x06005C58 RID: 23640 RVA: 0x001813E4 File Offset: 0x0017F5E4
		// (set) Token: 0x06005C59 RID: 23641 RVA: 0x001813F1 File Offset: 0x0017F5F1
		CookieContainer IEwsHttpWebRequest.CookieContainer
		{
			get
			{
				return this.request.CookieContainer;
			}
			set
			{
				this.request.CookieContainer = value;
			}
		}

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x06005C5A RID: 23642 RVA: 0x001813FF File Offset: 0x0017F5FF
		// (set) Token: 0x06005C5B RID: 23643 RVA: 0x0018140C File Offset: 0x0017F60C
		ICredentials IEwsHttpWebRequest.Credentials
		{
			get
			{
				return this.request.Credentials;
			}
			set
			{
				this.request.Credentials = value;
			}
		}

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x06005C5C RID: 23644 RVA: 0x0018141A File Offset: 0x0017F61A
		// (set) Token: 0x06005C5D RID: 23645 RVA: 0x00181427 File Offset: 0x0017F627
		WebHeaderCollection IEwsHttpWebRequest.Headers
		{
			get
			{
				return this.request.Headers;
			}
			set
			{
				this.request.Headers = value;
			}
		}

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x06005C5E RID: 23646 RVA: 0x00181435 File Offset: 0x0017F635
		// (set) Token: 0x06005C5F RID: 23647 RVA: 0x00181442 File Offset: 0x0017F642
		string IEwsHttpWebRequest.Method
		{
			get
			{
				return this.request.Method;
			}
			set
			{
				this.request.Method = value;
			}
		}

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x06005C60 RID: 23648 RVA: 0x00181450 File Offset: 0x0017F650
		// (set) Token: 0x06005C61 RID: 23649 RVA: 0x0018145D File Offset: 0x0017F65D
		IWebProxy IEwsHttpWebRequest.Proxy
		{
			get
			{
				return this.request.Proxy;
			}
			set
			{
				this.request.Proxy = value;
			}
		}

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x06005C62 RID: 23650 RVA: 0x0018146B File Offset: 0x0017F66B
		// (set) Token: 0x06005C63 RID: 23651 RVA: 0x00181478 File Offset: 0x0017F678
		bool IEwsHttpWebRequest.PreAuthenticate
		{
			get
			{
				return this.request.PreAuthenticate;
			}
			set
			{
				this.request.PreAuthenticate = value;
			}
		}

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x06005C64 RID: 23652 RVA: 0x00181486 File Offset: 0x0017F686
		Uri IEwsHttpWebRequest.RequestUri
		{
			get
			{
				return this.request.RequestUri;
			}
		}

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x06005C65 RID: 23653 RVA: 0x00181493 File Offset: 0x0017F693
		// (set) Token: 0x06005C66 RID: 23654 RVA: 0x001814A0 File Offset: 0x0017F6A0
		int IEwsHttpWebRequest.Timeout
		{
			get
			{
				return this.request.Timeout;
			}
			set
			{
				this.request.Timeout = value;
			}
		}

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x06005C67 RID: 23655 RVA: 0x001814AE File Offset: 0x0017F6AE
		// (set) Token: 0x06005C68 RID: 23656 RVA: 0x001814BB File Offset: 0x0017F6BB
		bool IEwsHttpWebRequest.UseDefaultCredentials
		{
			get
			{
				return this.request.UseDefaultCredentials;
			}
			set
			{
				this.request.UseDefaultCredentials = value;
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x06005C69 RID: 23657 RVA: 0x001814C9 File Offset: 0x0017F6C9
		// (set) Token: 0x06005C6A RID: 23658 RVA: 0x001814D6 File Offset: 0x0017F6D6
		string IEwsHttpWebRequest.UserAgent
		{
			get
			{
				return this.request.UserAgent;
			}
			set
			{
				this.request.UserAgent = value;
			}
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x06005C6B RID: 23659 RVA: 0x001814E4 File Offset: 0x0017F6E4
		// (set) Token: 0x06005C6C RID: 23660 RVA: 0x001814F1 File Offset: 0x0017F6F1
		public bool KeepAlive
		{
			get
			{
				return this.request.KeepAlive;
			}
			set
			{
				this.request.KeepAlive = value;
			}
		}

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x06005C6D RID: 23661 RVA: 0x001814FF File Offset: 0x0017F6FF
		// (set) Token: 0x06005C6E RID: 23662 RVA: 0x0018150C File Offset: 0x0017F70C
		public string ConnectionGroupName
		{
			get
			{
				return this.request.ConnectionGroupName;
			}
			set
			{
				this.request.ConnectionGroupName = value;
			}
		}

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x06005C6F RID: 23663 RVA: 0x0018151A File Offset: 0x0017F71A
		// (set) Token: 0x06005C70 RID: 23664 RVA: 0x00181527 File Offset: 0x0017F727
		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				return this.request.ServerCertificateValidationCallback;
			}
			set
			{
				this.request.ServerCertificateValidationCallback = value;
			}
		}

		// Token: 0x040032E5 RID: 13029
		private HttpWebRequest request;
	}
}
