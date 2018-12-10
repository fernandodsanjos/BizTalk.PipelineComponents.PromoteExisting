using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Streaming;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;
using BizTalkComponents.Utils;

namespace BizTalk.PipelineComponents.PromoteExisting
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("bc4ef586-bc79-4be9-b1d7-cda74953a468")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public partial class Promote : IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {


        [DisplayName("Name")]
        [Description("Context Name")]
        [RequiredRuntime]
        public string Property { get; set; }

        [DisplayName("Namespace")]
        [Description("Context Namespace")]
        [RequiredRuntime]
        public string Namespace { get; set; }

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            /*
           IBaseMessageContext context = pContext.GetMessageFactory().CreateMessageContext();

            for (int i = 0; i < pInMsg.Context.CountProperties; i++)
            {
                string name = String.Empty;
                string ns = String.Empty;

                object value = pInMsg.Context.ReadAt(i, out name, out ns);

                if (pInMsg.Context.IsPromoted(name, ns) || (name == Property && ns == Namespace))
                {
                    context.Promote(name, ns, value);
                }
                else
                {
                    context.Write(name, ns, value);
                }
                    
            }

            pInMsg.Context = context;
            */
            object value = pInMsg.Context.Read(Property, Namespace);

            if (value != null && pInMsg.Context.IsPromoted(Property, Namespace) == false)
                pInMsg.Context.Promote(Property, Namespace, value);

                return pInMsg;
        }

        //Load and Save are generic, the functions create properties based on the components "public" "read/write" properties.
        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var prop in props)

            {

                if (prop.CanRead & prop.CanWrite)

                {

                    prop.SetValue(this, PropertyBagHelper.ReadPropertyBag(propertyBag, prop.Name, prop.GetValue(this)));

                }

            }


        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            var props = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var prop in props)

            {

                if (prop.CanRead & prop.CanWrite)

                {

                    PropertyBagHelper.WritePropertyBag(propertyBag, prop.Name, prop.GetValue(this));

                }

            }

        }
    }
}
