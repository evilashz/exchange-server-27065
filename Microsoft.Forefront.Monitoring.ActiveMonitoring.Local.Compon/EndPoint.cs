using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200029A RID: 666
	public class EndPoint
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x000472B2 File Offset: 0x000454B2
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x000472BA File Offset: 0x000454BA
		public RequestMethod Method
		{
			get
			{
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x000472C3 File Offset: 0x000454C3
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x000472CB File Offset: 0x000454CB
		public bool AllowAutoRedirect
		{
			get
			{
				return this.allowAutoRedirect;
			}
			set
			{
				this.allowAutoRedirect = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x000472D4 File Offset: 0x000454D4
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x000472DC File Offset: 0x000454DC
		public bool GetHiddenInputValues
		{
			get
			{
				return this.getHiddenInputValues;
			}
			set
			{
				this.getHiddenInputValues = value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x000472E5 File Offset: 0x000454E5
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x000472ED File Offset: 0x000454ED
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x000472F6 File Offset: 0x000454F6
		// (set) Token: 0x0600163F RID: 5695 RVA: 0x000472FE File Offset: 0x000454FE
		public string FormName
		{
			get
			{
				return this.formName;
			}
			set
			{
				this.formName = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x00047307 File Offset: 0x00045507
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x0004730F File Offset: 0x0004550F
		public TimeSpan PageLoadTimeout
		{
			get
			{
				return this.pageLoadTimeout;
			}
			set
			{
				this.pageLoadTimeout = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x00047318 File Offset: 0x00045518
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x00047320 File Offset: 0x00045520
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x00047329 File Offset: 0x00045529
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x00047331 File Offset: 0x00045531
		public PostData PostData
		{
			get
			{
				return this.postData;
			}
			set
			{
				this.postData = value;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0004733A File Offset: 0x0004553A
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x00047342 File Offset: 0x00045542
		public ICollection<ExpectedResult> ExpectedResults
		{
			get
			{
				return this.expectedResults;
			}
			set
			{
				this.expectedResults = value;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0004734B File Offset: 0x0004554B
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x00047353 File Offset: 0x00045553
		public ICollection<ResponseCapture> Captures
		{
			get
			{
				return this.captures;
			}
			set
			{
				this.captures = value;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0004735C File Offset: 0x0004555C
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x00047364 File Offset: 0x00045564
		public string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
			internal set
			{
				this.authenticationType = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0004736D File Offset: 0x0004556D
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x00047375 File Offset: 0x00045575
		public string AuthenticationUser
		{
			get
			{
				return this.authenticationUser;
			}
			internal set
			{
				this.authenticationUser = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0004737E File Offset: 0x0004557E
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x00047386 File Offset: 0x00045586
		public string AuthenticationPassword
		{
			get
			{
				return this.authenticationPassword;
			}
			internal set
			{
				this.authenticationPassword = value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0004738F File Offset: 0x0004558F
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x00047397 File Offset: 0x00045597
		public bool SslValidation
		{
			get
			{
				return this.sslValidation;
			}
			internal set
			{
				this.sslValidation = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x000473A0 File Offset: 0x000455A0
		public Dictionary<string, string> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x000473A8 File Offset: 0x000455A8
		public static ICollection<EndPoint> FromXml(XmlNode workContext, TimeSpan defaultPageLoadTimeout)
		{
			List<EndPoint> list = new List<EndPoint>();
			using (XmlNodeList xmlNodeList = workContext.SelectNodes("//Endpoint"))
			{
				foreach (object obj in xmlNodeList)
				{
					XmlElement xmlElement = (XmlElement)obj;
					EndPoint endPoint = new EndPoint();
					endPoint.AllowAutoRedirect = Utils.GetBoolean(xmlElement.GetAttribute("AllowAutoRedirect"), "AllowAutoRedirect", true);
					string attribute = xmlElement.GetAttribute("Method");
					endPoint.Method = RequestMethod.Get;
					if (!string.IsNullOrWhiteSpace(attribute))
					{
						endPoint.Method = Utils.GetEnumValue<RequestMethod>(attribute, "Method");
					}
					attribute = xmlElement.GetAttribute("AuthenticationType");
					if (!string.IsNullOrWhiteSpace(attribute))
					{
						endPoint.AuthenticationType = attribute;
					}
					attribute = xmlElement.GetAttribute("AuthenticationUser");
					if (!string.IsNullOrWhiteSpace(attribute))
					{
						endPoint.AuthenticationUser = attribute;
					}
					attribute = xmlElement.GetAttribute("AuthenticationPassword");
					if (!string.IsNullOrWhiteSpace(attribute))
					{
						endPoint.AuthenticationPassword = attribute;
					}
					endPoint.SslValidation = Utils.GetBoolean(xmlElement.GetAttribute("SslValidation"), "SslValidation", true);
					attribute = xmlElement.GetAttribute("PageLoadTimeout");
					endPoint.PageLoadTimeout = defaultPageLoadTimeout;
					if (!string.IsNullOrWhiteSpace(attribute))
					{
						endPoint.PageLoadTimeout = TimeSpan.FromMilliseconds((double)Utils.GetPositiveInteger(attribute, "PageLoadTimeout"));
					}
					foreach (object obj2 in xmlElement.SelectNodes("Properties/Property"))
					{
						XmlElement xmlElement2 = (XmlElement)obj2;
						endPoint.Properties.Add(xmlElement2.GetAttribute("Name"), xmlElement2.GetAttribute("Value"));
					}
					if (endPoint.Method == RequestMethod.Post || endPoint.Method == RequestMethod.MSLiveLogin)
					{
						XmlAttribute xmlAttribute = xmlElement.Attributes["ContentType"];
						endPoint.ContentType = ((xmlAttribute == null) ? "application/x-www-form-urlencoded" : xmlAttribute.Value);
						attribute = xmlElement.GetAttribute("FormName");
						if (!string.IsNullOrWhiteSpace(attribute))
						{
							endPoint.FormName = attribute;
						}
						endPoint.PostData = PostData.FromXml(xmlElement);
					}
					endPoint.Uri = Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("Uri"), "Uri");
					endPoint.ExpectedResults = ExpectedResult.FromXml(xmlElement);
					endPoint.Captures = ResponseCapture.FromXml(xmlElement);
					list.Add(endPoint);
				}
			}
			if (list.Count == 0)
			{
				throw new ArgumentException("At least one EndPoint must be specified.");
			}
			return list;
		}

		// Token: 0x04000ACD RID: 2765
		private RequestMethod method;

		// Token: 0x04000ACE RID: 2766
		private bool allowAutoRedirect;

		// Token: 0x04000ACF RID: 2767
		private bool getHiddenInputValues;

		// Token: 0x04000AD0 RID: 2768
		private string contentType;

		// Token: 0x04000AD1 RID: 2769
		private string formName;

		// Token: 0x04000AD2 RID: 2770
		private TimeSpan pageLoadTimeout;

		// Token: 0x04000AD3 RID: 2771
		private string uri;

		// Token: 0x04000AD4 RID: 2772
		private PostData postData;

		// Token: 0x04000AD5 RID: 2773
		private string authenticationType;

		// Token: 0x04000AD6 RID: 2774
		private string authenticationUser;

		// Token: 0x04000AD7 RID: 2775
		private string authenticationPassword;

		// Token: 0x04000AD8 RID: 2776
		private bool sslValidation;

		// Token: 0x04000AD9 RID: 2777
		private Dictionary<string, string> properties = new Dictionary<string, string>();

		// Token: 0x04000ADA RID: 2778
		private ICollection<ExpectedResult> expectedResults;

		// Token: 0x04000ADB RID: 2779
		private ICollection<ResponseCapture> captures;
	}
}
