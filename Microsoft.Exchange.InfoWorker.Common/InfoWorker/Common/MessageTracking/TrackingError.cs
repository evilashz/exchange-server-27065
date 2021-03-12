using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002FE RID: 766
	internal sealed class TrackingError
	{
		// Token: 0x060016BE RID: 5822 RVA: 0x00069C30 File Offset: 0x00067E30
		internal TrackingError(ErrorCode errorCode, string target, string data, string exception) : this(Names<Microsoft.Exchange.InfoWorker.Common.MessageTracking.ErrorCode>.Map[(int)errorCode], ServerCache.Instance.GetLocalServer().Fqdn, ServerCache.Instance.GetLocalServer().Domain, target, data, exception)
		{
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00069C64 File Offset: 0x00067E64
		private TrackingError(string errorCodeString, string server, string domain, string target, string data, string exception)
		{
			this.properties = new Dictionary<string, string>(5);
			base..ctor();
			if (string.IsNullOrEmpty(errorCodeString))
			{
				throw new ArgumentNullException("errorCodeString");
			}
			if (string.IsNullOrEmpty(server))
			{
				throw new ArgumentNullException("server");
			}
			if (string.IsNullOrEmpty(domain))
			{
				throw new ArgumentNullException("domain");
			}
			this.properties["ErrorCode"] = errorCodeString;
			this.properties["Server"] = server;
			this.properties["Domain"] = domain;
			this.properties["Target"] = target;
			this.properties["Data"] = data;
			this.properties["Exception"] = exception;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00069D28 File Offset: 0x00067F28
		private TrackingError(ArrayOfTrackingPropertiesType propertyBag)
		{
			this.properties = new Dictionary<string, string>(5);
			base..ctor();
			foreach (TrackingPropertyType trackingPropertyType in propertyBag.Items)
			{
				this.properties[trackingPropertyType.Name] = trackingPropertyType.Value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00069D77 File Offset: 0x00067F77
		internal string ErrorCode
		{
			get
			{
				return this.properties["ErrorCode"];
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x00069D8C File Offset: 0x00067F8C
		internal string Target
		{
			get
			{
				string result;
				if (this.properties.TryGetValue("Target", out result))
				{
					return result;
				}
				return string.Empty;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00069DB4 File Offset: 0x00067FB4
		internal string Domain
		{
			get
			{
				return this.properties["Domain"];
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00069DC6 File Offset: 0x00067FC6
		internal string Server
		{
			get
			{
				return this.properties["Server"];
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00069DD8 File Offset: 0x00067FD8
		internal string Data
		{
			get
			{
				string result;
				if (this.properties.TryGetValue("Data", out result))
				{
					return result;
				}
				return string.Empty;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x00069E00 File Offset: 0x00068000
		internal Dictionary<string, string> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00069E08 File Offset: 0x00068008
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Error Details: ", this.properties.Count * 20);
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in this.properties)
			{
				stringBuilder.AppendFormat("{0}={1}", keyValuePair.Key, keyValuePair.Value);
				if (++num < this.properties.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00069EB0 File Offset: 0x000680B0
		internal static TrackingError CreateFromWSMessage(ArrayOfTrackingPropertiesType propertyBag)
		{
			string value = string.Empty;
			string value2 = string.Empty;
			string value3 = string.Empty;
			foreach (TrackingPropertyType trackingPropertyType in propertyBag.Items)
			{
				if (string.Equals(trackingPropertyType.Name, "ErrorCode", StringComparison.OrdinalIgnoreCase))
				{
					value = trackingPropertyType.Value;
				}
				else if (string.Equals(trackingPropertyType.Name, "Server", StringComparison.OrdinalIgnoreCase))
				{
					value2 = trackingPropertyType.Value;
				}
				else if (string.Equals(trackingPropertyType.Name, "Domain", StringComparison.OrdinalIgnoreCase))
				{
					value3 = trackingPropertyType.Value;
				}
			}
			if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value2) || string.IsNullOrEmpty(value3))
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "Error is not well formed. One or more of errorCode, server or domain is missing", new object[0]);
				return null;
			}
			TrackingError trackingError = new TrackingError(propertyBag);
			TraceWrapper.SearchLibraryTracer.TraceDebug<TrackingError>(0, "Decoded error: {0}", trackingError);
			return trackingError;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00069F90 File Offset: 0x00068190
		internal string ToErrorMessage(bool isMultiMessageSearch, out ErrorCodeInformationAttribute errorCodeInfo, out ErrorCode errorCode)
		{
			errorCodeInfo = null;
			string result;
			if (!EnumValidator<Microsoft.Exchange.InfoWorker.Common.MessageTracking.ErrorCode>.TryParse(this.ErrorCode, EnumParseOptions.Default, out errorCode))
			{
				result = (isMultiMessageSearch ? Strings.TrackingTransientErrorMultiMessageSearch : Strings.TrackingTransientError);
			}
			else
			{
				if (!EnumAttributeInfo<Microsoft.Exchange.InfoWorker.Common.MessageTracking.ErrorCode, ErrorCodeInformationAttribute>.TryGetValue((int)errorCode, out errorCodeInfo))
				{
					throw new InvalidOperationException(string.Format("{0} not annotated with ErrorCodeInformationAttribute", errorCode));
				}
				result = errorCodeInfo.ErrorFormatter(this, isMultiMessageSearch);
			}
			return result;
		}

		// Token: 0x04000E97 RID: 3735
		private const string ErrorCodeProperty = "ErrorCode";

		// Token: 0x04000E98 RID: 3736
		private const string ServerProperty = "Server";

		// Token: 0x04000E99 RID: 3737
		private const string DomainProperty = "Domain";

		// Token: 0x04000E9A RID: 3738
		private const string DataProperty = "Data";

		// Token: 0x04000E9B RID: 3739
		private const string ExceptionProperty = "Exception";

		// Token: 0x04000E9C RID: 3740
		private const string TargetProperty = "Target";

		// Token: 0x04000E9D RID: 3741
		private Dictionary<string, string> properties;
	}
}
