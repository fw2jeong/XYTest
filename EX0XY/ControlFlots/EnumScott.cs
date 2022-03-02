using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * @brief XY,FRA,Scope 네임스페이스
 * @author LeejeongHoon
 * @date 2022-02-28
 * @version 2.0.0
 */
namespace ControlFlots
{

   
    /// <summary>
    /// 그래프의 스타일을 지정하기 위한 Static 클래스 
    /// </summary>
    static public class Graphstyle
    {
        /// <summary>
        /// @brief XY,FRA,Scope의 그래프 스타일
        /// </summary>

        /// <summary>
        /// @brief Nomal Format 
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        static public string NullFormat(double y)
        {
            return Convert.ToString(y);
        }

        /// <summary>
        /// @brief Log Format
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        static public string LogFormat(double y)
        {
            if (y == Convert.ToInt32(y))
            {
                return Math.Pow(10, y).ToString("");
            }
            else
            {
                return "";
            }

        }
        /// <summary>
        /// @brief Time Scale
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        static public string TimeFormat(double y)
        {
            if (y == Convert.ToInt32(y))
            {
                return y.ToString("");
            }
            else
            {
                return "";
            }
        }


    }

    /// <summary>
    /// Plot의 상태를 알려주는 열거형입니다.
    /// PT_OK =0,PT_OVERFLOW =1
    /// </summary>
    public enum PLOT_STATUS
    {
        PT_OK = 0,
        PT_OVERFLOW = 1
    }


    /// <summary>
    /// @brief 채널을 나타냅니다.
    /// @note 채널은 총 6개 있습니다.
    /// </summary>
    public enum CH
    {
        ch1 = 0,
        ch2 = 1,
        ch3 = 2,
        ch4 = 3,
        ch5 = 4,
        ch6 = 5
    }

    /// <summary>
    /// @brief 영역을 나타냅니다.
    /// @note Current 영역과 Reference영역을 가지고 있습니다.
    /// </summary>
    public enum Source
    {
        Current = 0,
        Reference = 1
    }



    /// <summary>
    /// FRA에서 사용하는 열거형입니다.
    /// Magnitude,Phase이 있습니다.
    /// </summary>
    public enum DataType
    {
        Magnitude = 0,
        Phase = 1
    }
    /// <summary>
    /// Scope에서 사용하는 열거형입니다.
    /// Normal과 Roll이 있습니다.
    /// </summary>
    public enum DisplayMode
    {
        Normal=0,
        Roll=1
    }

    static public class TimeScale
    {


        readonly public static double InitAxisMag = 10000;

        /// <summary>
        /// @brief 기본 통신 속도에 대한 열거형 클래스입니다.
        /// @note Scope에서 사용하는 열거형입니다.
        /// </summary>

        /// <summary>
        /// @brief Scope chart의 시간축 그리드를 리턴
        /// @note 기본단위가 ms 단위임을 조심해야합니다.
        /// </summary>
        public enum GridInv
        {
            _1ms = 1,
            _2ms = 2,
            _5ms = 5,
            _10ms = 10,
            _20ms = 20,
            _50ms = 50,
            _100ms = 100,
            _200ms = 200,
            _500ms = 500,
            _1000ms = 1000
        }

        public enum CodeDivision
        {
            _1ms = 10,
            _2ms = 20,
            _5ms = 50,
            _10ms = 100,
            _20ms = 200,
            _50ms = 500,
            _100ms = 1000,
            _200ms = 2000,
            _500ms = 5000,
            _1000ms = 10000
        }


        /// <summary>
        /// @brief 통신속도에 대한 열거형입니다.
        /// @note 토마호크의 통신모드는 총 4개 존재합니다.
        /// </summary>
        public enum Mode
        {
            Mode1 = 24000,
            Mode2 = 12000,
            Mode4 = 10000,
            Mode6 = 5000
        }
    }


    static public class InitValue
    {
        /// <summary>
        /// @brief 초기에 차트의 내부 배열을 설정하기 위한 클래스입니다.
        /// </summary>
        public static int VOLUME = 240000;
    }
}
