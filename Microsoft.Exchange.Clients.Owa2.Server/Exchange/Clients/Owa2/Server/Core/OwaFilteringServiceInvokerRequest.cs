using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Filtering;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000226 RID: 550
	internal sealed class OwaFilteringServiceInvokerRequest : FilteringServiceInvokerRequest
	{
		// Token: 0x060014E7 RID: 5351 RVA: 0x0004A298 File Offset: 0x00048498
		private OwaFilteringServiceInvokerRequest(string organizationId, TimeSpan scanTimeout, int textScanLimit, MapiFipsDataStreamFilteringRequest mapiFipsDataStreamFilteringRequest) : base(organizationId, scanTimeout, textScanLimit, mapiFipsDataStreamFilteringRequest)
		{
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0004A2A8 File Offset: 0x000484A8
		public static OwaFilteringServiceInvokerRequest CreateInstance(Item item, IExtendedMapiFilteringContext context)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			string text = item.Session.OrganizationId.ToExternalDirectoryOrganizationId();
			if (string.IsNullOrEmpty(text))
			{
				text = Guid.Empty.ToString();
			}
			TimeSpan scanTimeout = OwaFilteringServiceInvokerRequest.GetScanTimeout(item, context);
			MapiFipsDataStreamFilteringRequest mapiFipsDataStreamFilteringRequest = MapiFipsDataStreamFilteringRequest.CreateInstance(item, context);
			return new OwaFilteringServiceInvokerRequest(text, scanTimeout, 153600, mapiFipsDataStreamFilteringRequest);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0004A31C File Offset: 0x0004851C
		private static TimeSpan GetScanTimeout(Item item, IExtendedMapiFilteringContext context)
		{
			List<KeyValuePair<string, double>> list = null;
			IList<AttachmentHandle> allHandles = item.AttachmentCollection.GetAllHandles();
			if (allHandles != null && allHandles.Count > 0)
			{
				foreach (AttachmentHandle handle in allHandles)
				{
					using (Attachment attachment = item.AttachmentCollection.Open(handle))
					{
						if (context.NeedsClassificationScan(attachment))
						{
							if (list == null)
							{
								list = new List<KeyValuePair<string, double>>();
							}
							list.Add(new KeyValuePair<string, double>(attachment.FileName, (double)attachment.Size));
						}
					}
				}
			}
			if (list == null && !context.NeedsClassificationScan())
			{
				return new TimeSpan(0L);
			}
			RulesScanTimeout rulesScanTimeout = new RulesScanTimeout(OwaFilteringServiceInvokerRequest.DefaultScanVelocities, 60000);
			return rulesScanTimeout.GetTimeout(context.NeedsClassificationScan() ? ((double)item.Body.Size) : 0.0, list);
		}

		// Token: 0x04000B51 RID: 2897
		private const int MinFipsTimeoutInMilliseconds = 60000;

		// Token: 0x04000B52 RID: 2898
		private const int MaxTextScanLimit = 153600;

		// Token: 0x04000B53 RID: 2899
		private static readonly Dictionary<string, uint> DefaultScanVelocities = new Dictionary<string, uint>
		{
			{
				".",
				30U
			},
			{
				"doc",
				1292U
			},
			{
				"docx",
				92U
			},
			{
				"xls",
				166U
			},
			{
				"xlsx",
				30U
			},
			{
				"ppt",
				7000U
			},
			{
				"pptx",
				400U
			},
			{
				"htm",
				120U
			},
			{
				"html",
				120U
			},
			{
				"pdf",
				840U
			}
		};
	}
}
