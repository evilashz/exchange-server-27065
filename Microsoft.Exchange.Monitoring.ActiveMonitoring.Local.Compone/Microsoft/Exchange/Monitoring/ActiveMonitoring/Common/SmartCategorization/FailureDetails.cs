using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common.SmartCategorization
{
	// Token: 0x02000103 RID: 259
	internal class FailureDetails
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0002F0B5 File Offset: 0x0002D2B5
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0002F0BD File Offset: 0x0002D2BD
		public Component Component { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0002F0C6 File Offset: 0x0002D2C6
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0002F0CE File Offset: 0x0002D2CE
		public FailureType FailureType { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0002F0D7 File Offset: 0x0002D2D7
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x0002F0DF File Offset: 0x0002D2DF
		public string Details { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0002F0E8 File Offset: 0x0002D2E8
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0002F0F0 File Offset: 0x0002D2F0
		public string Categorization { get; private set; }

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002F0F9 File Offset: 0x0002D2F9
		public FailureDetails()
		{
			this.FailureType = FailureType.Unrecognized;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0002F108 File Offset: 0x0002D308
		public FailureDetails(FailureType failureType, Component faultedComponent) : this(failureType, faultedComponent, string.Empty, string.Empty)
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0002F11C File Offset: 0x0002D31C
		public FailureDetails(FailureType failureType, Component faultedComponent, string details, string categorization)
		{
			if (faultedComponent == null)
			{
				throw new ArgumentNullException("faultedComponent");
			}
			if (details == null)
			{
				throw new ArgumentNullException("details");
			}
			this.FailureType = failureType;
			this.Component = faultedComponent;
			this.Details = details;
			this.Categorization = categorization;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0002F170 File Offset: 0x0002D370
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Details))
			{
				return this.Details;
			}
			if (this.FailureType == FailureType.Unrecognized || this.Component == null)
			{
				return SCStrings.UnrecognizedFailure;
			}
			return string.Format(SCStrings.FailureDetails, this.FailureType.ToString(), this.Component.ShortName);
		}
	}
}
