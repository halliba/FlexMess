

import javax.swing.*;
import javax.swing.border.EmptyBorder;
import java.awt.*;
import java.awt.event.*;

public class Notification {

    Timer timer;
    final JFrame frame = new JFrame();
    JLabel label = new JLabel();
    JPanel panel = new JPanel();
    int timeout = 15_000;

    public Notification() {
        frame.add(panel);
        panel.add(label);
        panel.setBorder(new EmptyBorder(10,10,10,10));


        frame.dispose();
        frame.setUndecorated(true); // Remove title bar
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setDefaultLookAndFeelDecorated(false);

        frame.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                frame.setVisible(false);
            }


        });
    }


    public void displayTray(String msg) {
        label.setText("<html>" + msg.replaceAll("<","&lt;").replaceAll(">", "&gt;").replaceAll("\n", "<br/>") + "</html>");
        frame.pack();
        pos();
        frame.setVisible(true);
        Toolkit.getDefaultToolkit().beep();


        timer = new Timer(timeout, evt -> {
            frame.setVisible(false);
            timer.stop();
        });

        timer.start();
    }

    private void pos(){
        GraphicsEnvironment ge = GraphicsEnvironment.getLocalGraphicsEnvironment();
        GraphicsDevice defaultScreen = ge.getDefaultScreenDevice();
        Rectangle rect = defaultScreen.getDefaultConfiguration().getBounds();
        int x = (int) rect.getMaxX() - (frame.getWidth() + 10);
        int y = (int) rect.getMaxY() - (frame.getHeight() + 10);
        frame.setLocation(x, y);
    }
}
