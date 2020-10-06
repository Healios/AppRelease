using HotChocolate.Language;
using HotChocolate.Types;
using System;

namespace GraphQL.Models.Scalars
{
    public class ByteArrayType : ScalarType
    {
        public ByteArrayType() : base("ByteArray")
        {

        }

        public override Type ClrType => typeof(byte[]);

        public override bool IsInstanceOfType(IValueNode literal)
        {
            if (literal == null)
                throw new ArgumentNullException(nameof(literal));

            return literal is StringValueNode;
        }

        public override object ParseLiteral(IValueNode literal)
        {
            if (literal == null)
                throw new ArgumentNullException(nameof(literal));

            if (literal is StringValueNode stringLiteral)
                return Convert.FromBase64String(stringLiteral.Value);

            if (literal is NullValueNode)
                return null;

            throw new ArgumentException("The byte array type can only parse string literals", nameof(literal));
        }

        public override IValueNode ParseValue(object value)
        {
            if (value == null)
                return new NullValueNode(null);

            if (value is byte[] bytes)
                return new StringValueNode(null, Convert.ToBase64String(bytes), false);

            throw new ArgumentException("The specified value has to be a byte array in order to be parsed by the string type.");
        }

        public override object Serialize(object value)
        {
            if (value == null)
                return null;

            if (value is byte[] bytes)
                return Convert.ToBase64String(bytes);

            throw new ArgumentException("The specified value cannot be serialized by the ByteArrayType.");
        }

        public override bool TryDeserialize(object serialized, out object value)
        {
            if (serialized is null)
            {
                value = null;
                return true;
            }

            if (serialized is string s)
            {
                value = Convert.FromBase64String(s);
                return true;
            }

            if (serialized is byte[] b)
            {
                value = b;
                return true;
            }

            value = null;
            return false;
        }
    }
}
