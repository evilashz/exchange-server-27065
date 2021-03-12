using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B86 RID: 2950
	internal class Binding : CustomBinding
	{
		// Token: 0x06003F2B RID: 16171 RVA: 0x000A6FBD File Offset: 0x000A51BD
		protected internal Binding()
		{
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x000A6FC5 File Offset: 0x000A51C5
		protected internal Binding(BindingElementCollection sourceBindingElementCollection) : base(sourceBindingElementCollection)
		{
			ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)this.GetHashCode(), "Xrop Binding initialized");
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x000A6FE4 File Offset: 0x000A51E4
		internal static BindingElementCollection GetClientBindingElements()
		{
			return Binding.ClientBinding.CreateBindingElements("XropLiveIdFederatedBinding");
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x000A6FF0 File Offset: 0x000A51F0
		internal static BindingElementCollection GetListenerBindingElements()
		{
			return Binding.ListenerBinding.CreateBindingElements("XropLiveIdFederatedBinding");
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x000A6FFC File Offset: 0x000A51FC
		internal void SetMaxTimeout(TimeSpan timeSpan)
		{
			base.CloseTimeout = timeSpan;
			base.OpenTimeout = timeSpan;
			base.ReceiveTimeout = timeSpan;
			base.SendTimeout = timeSpan;
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x000A701A File Offset: 0x000A521A
		internal void SetMaxTimeout()
		{
			this.SetMaxTimeout(TimeSpan.MaxValue);
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x000A7028 File Offset: 0x000A5228
		protected static WSHttpBindingBase InitializeDefaultTemplateBinding(string bindingTypeConfigurationKey)
		{
			WS2007HttpBinding ws2007HttpBinding = new WS2007HttpBinding(SecurityMode.TransportWithMessageCredential, false);
			ws2007HttpBinding.Security.Message.EstablishSecurityContext = true;
			ws2007HttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.IssuedToken;
			ws2007HttpBinding.Security.Message.NegotiateServiceCredential = false;
			ws2007HttpBinding.AllowCookies = false;
			ws2007HttpBinding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
			ws2007HttpBinding.Security.Transport.Realm = string.Empty;
			if (bindingTypeConfigurationKey == "XropLiveIdFederatedBinding")
			{
				ws2007HttpBinding.Security.Message.AlgorithmSuite = SecurityAlgorithmSuite.TripleDes;
			}
			return ws2007HttpBinding;
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x000A70B8 File Offset: 0x000A52B8
		protected static BindingElementCollection InitializeBindingElements(BindingElementCollection sourceBindingElementCollection)
		{
			HttpsTransportBindingElement httpsTransportBindingElement = sourceBindingElementCollection.Find<HttpsTransportBindingElement>();
			if (httpsTransportBindingElement == null)
			{
				ExTraceGlobals.XropServiceClientTracer.TraceError(0L, "Invalid Xrop Service Binding, missing required HTTPS binding element");
				throw new InvalidOperationException();
			}
			TransportSecurityBindingElement transportSecurityBindingElement = sourceBindingElementCollection.Find<TransportSecurityBindingElement>();
			if (transportSecurityBindingElement == null)
			{
				ExTraceGlobals.XropServiceClientTracer.TraceError(0L, "Xrop Service Transport Security Binding missing");
				throw new InvalidOperationException();
			}
			Binding.ConfigureDefaultHttpTransportSettings(httpsTransportBindingElement);
			Binding.ConfigureValidateSecuritySettings(transportSecurityBindingElement);
			MessageEncodingBindingElement messageEncodingBindingElement;
			if (!Binding.UseTextMessageEncoder.Value)
			{
				messageEncodingBindingElement = sourceBindingElementCollection.Find<BinaryMessageEncodingBindingElement>();
				if (messageEncodingBindingElement == null)
				{
					messageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
					Binding.ConfigureDefaultBinaryXmlSettings(messageEncodingBindingElement as BinaryMessageEncodingBindingElement);
				}
			}
			else
			{
				messageEncodingBindingElement = sourceBindingElementCollection.Find<TextMessageEncodingBindingElement>();
				if (messageEncodingBindingElement == null)
				{
					messageEncodingBindingElement = new TextMessageEncodingBindingElement();
				}
			}
			return new BindingElementCollection
			{
				transportSecurityBindingElement,
				messageEncodingBindingElement,
				httpsTransportBindingElement
			};
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x000A716A File Offset: 0x000A536A
		private static void ConfigureDefaultHttpTransportSettings(HttpsTransportBindingElement transportBindingElement)
		{
			transportBindingElement.RequireClientCertificate = false;
			transportBindingElement.AuthenticationScheme = AuthenticationSchemes.Anonymous;
			transportBindingElement.ProxyAuthenticationScheme = AuthenticationSchemes.Anonymous;
			transportBindingElement.UnsafeConnectionNtlmAuthentication = false;
			transportBindingElement.TransferMode = TransferMode.Buffered;
			transportBindingElement.KeepAliveEnabled = true;
			transportBindingElement.ManualAddressing = false;
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000A71A8 File Offset: 0x000A53A8
		private static void ConfigureValidateSecuritySettings(TransportSecurityBindingElement transporSecuritytBindingElement)
		{
			SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = null;
			foreach (SecurityTokenParameters securityTokenParameters in transporSecuritytBindingElement.EndpointSupportingTokenParameters.Endorsing)
			{
				if (securityTokenParameters is SecureConversationSecurityTokenParameters)
				{
					secureConversationSecurityTokenParameters = (securityTokenParameters as SecureConversationSecurityTokenParameters);
					break;
				}
			}
			if (secureConversationSecurityTokenParameters == null)
			{
				ExTraceGlobals.XropServiceClientTracer.TraceError(0L, "Xrop Service Transport Security Binding missing Secure Conversation Parameters");
				throw new InvalidOperationException();
			}
			TransportSecurityBindingElement transportSecurityBindingElement = secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement as TransportSecurityBindingElement;
			if (transportSecurityBindingElement == null)
			{
				ExTraceGlobals.XropServiceClientTracer.TraceError(0L, "Xrop Service Secure Conversation Token Parameters missing bootstrap security binding element");
				throw new InvalidOperationException();
			}
			secureConversationSecurityTokenParameters.RequireCancellation = true;
			secureConversationSecurityTokenParameters.RequireDerivedKeys = true;
			bool flag = false;
			foreach (SecurityTokenParameters securityTokenParameters2 in transporSecuritytBindingElement.EndpointSupportingTokenParameters.Signed)
			{
				if (securityTokenParameters2 is UserNameSecurityTokenParameters)
				{
					flag = true;
					break;
				}
			}
			if (!flag && !Binding.DoNotSendUserNameSecurityToken.Value)
			{
				UserNameSecurityTokenParameters item = new UserNameSecurityTokenParameters();
				transportSecurityBindingElement.EndpointSupportingTokenParameters.Signed.Add(item);
			}
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x000A72D0 File Offset: 0x000A54D0
		private static void ConfigureDefaultBinaryXmlSettings(BinaryMessageEncodingBindingElement messageEncodingBindingElement)
		{
			messageEncodingBindingElement.MaxReadPoolSize = 64;
			messageEncodingBindingElement.MaxWritePoolSize = 64;
			messageEncodingBindingElement.ReaderQuotas.MaxDepth = 64;
			messageEncodingBindingElement.ReaderQuotas.MaxStringContentLength = 1048576;
			messageEncodingBindingElement.ReaderQuotas.MaxArrayLength = 1048576;
			messageEncodingBindingElement.ReaderQuotas.MaxBytesPerRead = 1048576;
			messageEncodingBindingElement.ReaderQuotas.MaxNameTableCharCount = 16384;
		}

		// Token: 0x04003723 RID: 14115
		internal const string DiagInfo = "X-DiagInfo";

		// Token: 0x04003724 RID: 14116
		private const string LiveIdFederatedBinding = "XropLiveIdFederatedBinding";

		// Token: 0x04003725 RID: 14117
		internal static readonly BoolAppSettingsEntry UseTextMessageEncoder = new BoolAppSettingsEntry("UseTextMessageEncoder", false, null);

		// Token: 0x04003726 RID: 14118
		internal static readonly BoolAppSettingsEntry DoNotSendUserNameSecurityToken = new BoolAppSettingsEntry("DoNotSendUserNameSecurityToken", false, null);

		// Token: 0x04003727 RID: 14119
		internal static readonly BoolAppSettingsEntry DoNotCacheFactories = new BoolAppSettingsEntry("DoNotCacheFactories", false, null);

		// Token: 0x04003728 RID: 14120
		internal static readonly BoolAppSettingsEntry AddSessionIdToQueryString = new BoolAppSettingsEntry("AddSessionIdToQueryString", true, null);

		// Token: 0x04003729 RID: 14121
		internal static readonly BoolAppSettingsEntry IncludeErrorDetailsInTrace = new BoolAppSettingsEntry("IncludeErrorDetailsInTrace", true, null);

		// Token: 0x0400372A RID: 14122
		internal static readonly BoolAppSettingsEntry IncludeDetailsInServiceFaults = new BoolAppSettingsEntry("IncludeDetailsInServiceFaults", true, null);

		// Token: 0x0400372B RID: 14123
		internal static readonly BoolAppSettingsEntry IncludeStackInServiceFaults = new BoolAppSettingsEntry("IncludeStackInServiceFaults", true, null);

		// Token: 0x0400372C RID: 14124
		internal static readonly BoolAppSettingsEntry Use200ForSoapFaults = new BoolAppSettingsEntry("Use200ForSoapFaults", true, null);

		// Token: 0x0400372D RID: 14125
		internal static readonly BoolAppSettingsEntry UseHttpListenerExtendedErrorLogging = new BoolAppSettingsEntry("UseHttpListenerExtendedErrorLogging", true, null);

		// Token: 0x0400372E RID: 14126
		internal static readonly BoolAppSettingsEntry UseWCFTransportExceptionHandler = new BoolAppSettingsEntry("UseWCFTransportExceptionHandler", true, null);

		// Token: 0x02000B87 RID: 2951
		private class ClientBinding : Binding
		{
			// Token: 0x06003F37 RID: 16183 RVA: 0x000A73F4 File Offset: 0x000A55F4
			internal static BindingElementCollection CreateBindingElements(string bindingTypeConfigurationKey)
			{
				try
				{
					CustomBinding customBinding = new CustomBinding(bindingTypeConfigurationKey);
					return customBinding.CreateBindingElements();
				}
				catch (ConfigurationErrorsException)
				{
					ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)bindingTypeConfigurationKey.GetHashCode(), "No external Xrop WCF configuration found");
				}
				catch (KeyNotFoundException)
				{
					ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)bindingTypeConfigurationKey.GetHashCode(), "No external Xrop WCF configuration found");
				}
				WSHttpBindingBase wshttpBindingBase = Binding.InitializeDefaultTemplateBinding(bindingTypeConfigurationKey);
				wshttpBindingBase.ProxyAddress = null;
				wshttpBindingBase.UseDefaultWebProxy = true;
				wshttpBindingBase.BypassProxyOnLocal = false;
				Binding.ClientBinding.SetThrottling(wshttpBindingBase);
				return Binding.InitializeBindingElements(wshttpBindingBase.CreateBindingElements());
			}

			// Token: 0x06003F38 RID: 16184 RVA: 0x000A7490 File Offset: 0x000A5690
			private static void SetThrottling(WSHttpBindingBase binding)
			{
				binding.MaxReceivedMessageSize = 1048576L;
				binding.ReceiveTimeout = TimeSpan.FromMinutes(10.0);
				binding.SendTimeout = TimeSpan.FromMinutes(10.0);
				Binding.ClientBinding.SetThrottling(binding.ReaderQuotas);
			}

			// Token: 0x06003F39 RID: 16185 RVA: 0x000A74DC File Offset: 0x000A56DC
			private static void SetThrottling(XmlDictionaryReaderQuotas readerQuotas)
			{
				readerQuotas.MaxDepth = 64;
				readerQuotas.MaxStringContentLength = 1048576;
				readerQuotas.MaxArrayLength = 1048576;
				readerQuotas.MaxBytesPerRead = 1048576;
				readerQuotas.MaxNameTableCharCount = 16384;
			}

			// Token: 0x06003F3A RID: 16186 RVA: 0x000A7512 File Offset: 0x000A5712
			private static void ConfigureSystemDotNetBehaviors()
			{
			}
		}

		// Token: 0x02000B88 RID: 2952
		private class ListenerBinding : Binding
		{
			// Token: 0x06003F3C RID: 16188 RVA: 0x000A751C File Offset: 0x000A571C
			internal static BindingElementCollection CreateBindingElements(string bindingTypeConfigurationKey)
			{
				try
				{
					CustomBinding customBinding = new CustomBinding(bindingTypeConfigurationKey);
					return customBinding.CreateBindingElements();
				}
				catch (ConfigurationErrorsException)
				{
					ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)bindingTypeConfigurationKey.GetHashCode(), "No external Xrop WCF configuration found");
				}
				catch (KeyNotFoundException)
				{
					ExTraceGlobals.XropServiceClientTracer.TraceDebug((long)bindingTypeConfigurationKey.GetHashCode(), "No external Xrop WCF configuration found");
				}
				WSHttpBindingBase wshttpBindingBase = Binding.InitializeDefaultTemplateBinding(bindingTypeConfigurationKey);
				Binding.ListenerBinding.SetThrottling(wshttpBindingBase);
				return Binding.InitializeBindingElements(wshttpBindingBase.CreateBindingElements());
			}

			// Token: 0x06003F3D RID: 16189 RVA: 0x000A75A4 File Offset: 0x000A57A4
			private static void SetThrottling(WSHttpBindingBase binding)
			{
				binding.MaxReceivedMessageSize = 1048576L;
				binding.ReceiveTimeout = TimeSpan.FromMinutes(10.0);
				binding.SendTimeout = TimeSpan.FromMinutes(10.0);
				Binding.ListenerBinding.SetThrottling(binding.ReaderQuotas);
			}

			// Token: 0x06003F3E RID: 16190 RVA: 0x000A75F0 File Offset: 0x000A57F0
			private static void SetThrottling(XmlDictionaryReaderQuotas readerQuotas)
			{
				readerQuotas.MaxDepth = 64;
				readerQuotas.MaxStringContentLength = 1048576;
				readerQuotas.MaxArrayLength = 1048576;
				readerQuotas.MaxBytesPerRead = 1048576;
				readerQuotas.MaxNameTableCharCount = 16384;
			}

			// Token: 0x06003F3F RID: 16191 RVA: 0x000A7626 File Offset: 0x000A5826
			private static void ConfigureSystemDotNetBehaviors()
			{
			}
		}
	}
}
