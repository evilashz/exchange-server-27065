using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004E8 RID: 1256
	[OwaEventNamespace("MessageAnnotation")]
	internal sealed class MessageAnnotationEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002FB6 RID: 12214 RVA: 0x0011549D File Offset: 0x0011369D
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(MessageAnnotationEventHandler));
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x001154B0 File Offset: 0x001136B0
		[OwaEvent("GetMessageAnnotationInternals")]
		[OwaEventParameter("sId", typeof(string))]
		public void GetMessageAnnotationInternals()
		{
			string idString = (string)base.GetParameter("sId");
			string divErrorId = "divFPErr";
			this.Writer.Write("<div id=\"divFPTrR\">");
			Infobar infobar = new Infobar(divErrorId, "infobar");
			infobar.Render(this.Writer);
			MessageAnnotationHost.RenderMessageAnnotationDivStart(this.Writer, "msgnotediv");
			string messageNoteText = string.Empty;
			PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
			{
				MessageItemSchema.MessageAnnotation
			};
			using (MessageItem item = Utilities.GetItem<MessageItem>(base.UserContext, idString, prefetchProperties))
			{
				object obj = item.TryGetProperty(MessageItemSchema.MessageAnnotation);
				if (obj is string)
				{
					messageNoteText = (obj as string);
				}
			}
			MessageAnnotationHost.RenderMessageAnnotationControl(this.Writer, "msgnotectrl", messageNoteText);
			MessageAnnotationHost.RenderMessageAnnotationDivEnd(this.Writer);
			this.Writer.Write("</div>");
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x001155A0 File Offset: 0x001137A0
		[OwaEventParameter("sId", typeof(string))]
		[OwaEvent("SaveMessageAnnotation")]
		[OwaEventParameter("svMsgAnnotation", typeof(string))]
		public void SaveMessageAnnotation()
		{
			PropertyDefinition[] propertyDefinitions = new PropertyDefinition[]
			{
				MessageItemSchema.MessageAnnotation
			};
			string idString = (string)base.GetParameter("sId");
			string text = (string)base.GetParameter("svMsgAnnotation");
			using (MessageItem item = Utilities.GetItem<MessageItem>(base.UserContext, idString, new PropertyDefinition[]
			{
				MessageItemSchema.MessageAnnotation
			}))
			{
				item.OpenAsReadWrite();
				item.SetProperties(propertyDefinitions, new object[]
				{
					text
				});
				item.Save(SaveMode.NoConflictResolutionForceSave);
			}
		}

		// Token: 0x04002182 RID: 8578
		public const string EventNamespace = "MessageAnnotation";

		// Token: 0x04002183 RID: 8579
		public const string MethodGetMessageAnnotationInternals = "GetMessageAnnotationInternals";

		// Token: 0x04002184 RID: 8580
		public const string MethodSaveMessageAnnotation = "SaveMessageAnnotation";

		// Token: 0x04002185 RID: 8581
		public const string StoreObjectId = "sId";

		// Token: 0x04002186 RID: 8582
		public const string MessageAnnotation = "svMsgAnnotation";
	}
}
