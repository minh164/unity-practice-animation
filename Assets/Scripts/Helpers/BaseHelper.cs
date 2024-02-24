namespace Helpers
{
    public class BaseHelper
    {
        /// <summary>
        /// Determines whether a float value is in float range.
        /// </summary>
        /// <param name="needle"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool InFloatRange(float needle, float from, float to)
        {
            return needle >= from && needle <= to;
        }
    }
}