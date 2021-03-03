package com.example.towerbuilderspring.controller;

import com.example.towerbuilderspring.model.Users;
import com.example.towerbuilderspring.repository.UserModelRepository;
import com.example.towerbuilderspring.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("api/Auth/")
public class UserLoginController {

    @Autowired
    UserRepository userRepository;

    @Autowired
    UserModelRepository userModelRepository;

    private PasswordEncoder encoder = new BCryptPasswordEncoder();

    @GetMapping("Login/{userName}/{password}")
    public ResponseEntity<Users>  authenticateUser(@PathVariable String userName,
                                                   @PathVariable String password)  throws AssertionError {
        try
        {
            Users user = userRepository.findByUserName(userName);
            System.out.println(user);

            // Todo Make sure it checks the encoder before sending the data.
            if (user != null) {
                System.out.println("The user was found");
                System.out.println(encoder.matches(password, user.getPassword()));
                return new ResponseEntity<>(user,HttpStatus.OK);
            }
            throw new AssertionError("The username or password is incorrect");
        }
        catch (AssertionError e) {
            return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PostMapping("SignUp/")
    public ResponseEntity<Object> createUser(@RequestBody Users user) {
        try {

            String encryptedPassword = encoder.encode(user.getPassword());
            Users newUser = new Users(user.getUserName(), user.getEmail(), encryptedPassword, user.getTotalExp());
            userRepository.save(newUser);
            return new ResponseEntity<>(user, HttpStatus.CREATED);
        }
        catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    // Todo Once Roles have been successfully implemented make sure only the admin can delete a user.
    @DeleteMapping("Delete/{username}/")
    public ResponseEntity<Users> deleteUser(@PathVariable("username") String username) {
        try {
            Users userToDelete = userRepository.findByUserName(username);

            System.out.println(userToDelete.toString());
            if (userToDelete != null) {
                userRepository.delete(userToDelete);
                return new ResponseEntity<>(userToDelete, HttpStatus.OK);
            }
            else {
                return new ResponseEntity<>(null, HttpStatus.NOT_FOUND);
            }
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

}