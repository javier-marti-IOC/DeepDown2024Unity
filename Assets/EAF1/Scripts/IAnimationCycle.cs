/**
 * Interficie per implementar cicles d'animació start-connect-end (els atacs per exemple)
 */
public interface IAnimationCycle
{
    void OnAnimationEnd();
    void OnAnimationStart();
    void OnAnimationConnect();
}