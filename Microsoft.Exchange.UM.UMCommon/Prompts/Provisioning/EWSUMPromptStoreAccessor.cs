using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x02000085 RID: 133
	internal class EWSUMPromptStoreAccessor : DisposableBase, IUMPromptStorage, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0000FD98 File Offset: 0x0000DF98
		public EWSUMPromptStoreAccessor(ExchangePrincipal user, Guid configurationObject)
		{
			EWSUMPromptStoreAccessor <>4__this = this;
			ExAssert.RetailAssert(configurationObject != Guid.Empty, "ConfigurationObject Guid cannot be empty");
			this.tracer = new DiagnosticHelper(this, ExTraceGlobals.XsoTracer);
			PIIMessage pii = PIIMessage.Create(PIIType._SmtpAddress, user.MailboxInfo.PrimarySmtpAddress.ToString());
			this.tracer.Trace(pii, "EWSUMPromptStoreAccessor for configObject {0}, user: _PrimarySmtpAddress", new object[]
			{
				configurationObject
			});
			this.configurationObject = configurationObject;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				<>4__this.ewsBinding = new UMMailboxAccessorEwsBinding(user, <>4__this.tracer);
				<>4__this.tracer.Trace("EWSUMPromptStoreAccessor, EWS Url = {0}", new object[]
				{
					<>4__this.ewsBinding.Url
				});
			}, this.tracer);
			this.CheckForErrors(e);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000FE5D File Offset: 0x0000E05D
		public void DeleteAllPrompts()
		{
			this.tracer.Trace("EWSUMPromptStoreAccessor : DeleteAllPrompts", new object[0]);
			this.InternalDeletePrompts(null);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000FE7C File Offset: 0x0000E07C
		public void DeletePrompts(string[] prompts)
		{
			this.tracer.Trace("EWSUMPromptStoreAccessor : DeletePrompts", new object[0]);
			ValidateArgument.NotNull(prompts, "Prompts");
			this.InternalDeletePrompts(prompts);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000FEA6 File Offset: 0x0000E0A6
		public string[] GetPromptNames()
		{
			return this.GetPromptNames(TimeSpan.Zero);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000FEDC File Offset: 0x0000E0DC
		public string[] GetPromptNames(TimeSpan timeSinceLastModified)
		{
			this.tracer.Trace("EWSUMPromptStoreAccessor : GetPromptNames, for Guid {0}", new object[]
			{
				this.configurationObject
			});
			GetUMPromptNamesType request = new GetUMPromptNamesType();
			request.ConfigurationObject = this.configurationObject.ToString();
			request.HoursElapsedSinceLastModified = (int)timeSinceLastModified.TotalHours;
			GetUMPromptNamesResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.GetUMPromptNames(request);
			}, this.tracer);
			this.CheckResponse(e, response, null);
			this.tracer.Trace("EWSUMPromptStoreAccessor : GetPromptNames, Number of Prompts {0}", new object[]
			{
				response.PromptNames.Length
			});
			return response.PromptNames;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		public void CreatePrompt(string promptName, string audioBytes)
		{
			ValidateArgument.NotNullOrEmpty(promptName, "promptName");
			ExAssert.RetailAssert(audioBytes != null && audioBytes.Length > 0, "AudioData passed cannot be null or empty");
			this.tracer.Trace("EWSUMPromptStoreAccessor : CreatePrompt, promptName {0}", new object[]
			{
				promptName
			});
			CreateUMPromptType request = new CreateUMPromptType();
			request.ConfigurationObject = this.configurationObject.ToString();
			request.PromptName = promptName;
			request.AudioData = Convert.FromBase64String(audioBytes);
			CreateUMPromptResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.CreateUMPrompt(request);
			}, this.tracer);
			this.CheckResponse(e, response, promptName);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000100DC File Offset: 0x0000E2DC
		public string GetPrompt(string promptName)
		{
			ValidateArgument.NotNullOrEmpty(promptName, "promptName");
			this.tracer.Trace("EWSUMPromptStoreAccessor : GetPrompt, promptName {0}", new object[]
			{
				promptName
			});
			GetUMPromptType request = new GetUMPromptType();
			request.ConfigurationObject = this.configurationObject.ToString();
			request.PromptName = promptName;
			GetUMPromptResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.GetUMPrompt(request);
			}, this.tracer);
			this.CheckResponse(e, response, promptName);
			this.tracer.Trace("EWSUMPromptStoreAccessor : GetPrompt, AduioData Length {0}", new object[]
			{
				response.AudioData.Length
			});
			return Convert.ToBase64String(response.AudioData);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000101C0 File Offset: 0x0000E3C0
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.tracer.Trace("EWSUMPromptsStorage : InternalDispose", new object[0]);
				if (this.ewsBinding != null)
				{
					this.ewsBinding.Dispose();
				}
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000101EE File Offset: 0x0000E3EE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EWSUMPromptStoreAccessor>(this);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001021C File Offset: 0x0000E41C
		private void InternalDeletePrompts(string[] prompts)
		{
			DeleteUMPromptsType request = new DeleteUMPromptsType();
			request.ConfigurationObject = this.configurationObject.ToString();
			if (prompts != null)
			{
				request.PromptNames = prompts;
			}
			DeleteUMPromptsResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.DeleteUMPrompts(request);
			}, this.tracer);
			this.CheckResponse(e, response, null);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001029C File Offset: 0x0000E49C
		private void CheckForErrors(Exception e)
		{
			if (e != null)
			{
				this.tracer.Trace("EWSUMPromptStoreAccessor : CheckForErrors, Exception: {0}", new object[]
				{
					e
				});
				throw new PublishingPointException(e.Message, e);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000102D8 File Offset: 0x0000E4D8
		private void CheckResponse(Exception e, ResponseMessageType response, string promptName)
		{
			this.CheckForErrors(e);
			if (response == null)
			{
				this.tracer.Trace("EWSUMPromptStoreAccessor : CheckResponse, response == null", new object[0]);
				throw new EWSUMMailboxAccessException(Strings.EWSNoResponseReceived);
			}
			this.tracer.Trace("EWSUMPromptStoreAccessor : CheckResponse, ResponseCode = {0}, ResponseClass = {1}, MessageText = {2}", new object[]
			{
				response.ResponseCode,
				response.ResponseClass,
				response.MessageText
			});
			if (response.ResponseClass == ResponseClassType.Success && response.ResponseCode == ResponseCodeType.NoError)
			{
				return;
			}
			ResponseCodeType responseCode = response.ResponseCode;
			if (responseCode == ResponseCodeType.ErrorDeleteUnifiedMessagingPromptFailed)
			{
				throw new DeleteContentException(string.Empty);
			}
			if (responseCode == ResponseCodeType.ErrorPromptPublishingOperationFailed)
			{
				throw new PublishingPointException(response.MessageText);
			}
			if (responseCode != ResponseCodeType.ErrorUnifiedMessagingPromptNotFound)
			{
				throw new EWSUMMailboxAccessException(Strings.EWSOperationFailed(response.ResponseCode.ToString(), response.MessageText));
			}
			throw new SourceFileNotFoundException(promptName);
		}

		// Token: 0x040002FA RID: 762
		private readonly Guid configurationObject;

		// Token: 0x040002FB RID: 763
		private UMMailboxAccessorEwsBinding ewsBinding;

		// Token: 0x040002FC RID: 764
		private DiagnosticHelper tracer;
	}
}
